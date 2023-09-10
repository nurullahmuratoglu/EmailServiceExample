using EmailServiceExample.API.Contracts;
using EmailServiceExample.API.DTOs;
using EmailServiceExample.API.Mapping;
using EmailServiceExample.API.Model;
using EmailServiceExample.API.OptionsModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace EmailServiceExample.API.Services
{
    public class EmailService : IEmailService
    {
        private readonly AppDbContext _appDbContext;
        private readonly EmailSettings _emailSettings;


        public EmailService(AppDbContext appDbContext, IOptions<EmailSettings> options)
        {
            _appDbContext = appDbContext;
            _emailSettings = options.Value;
        }
        public async Task<ResponseDto<NoDataDto>> AddEmailAsync(CreateEmailDto request)
        {
            if (GetEmails().Any(x => x.EmailAddress == request.EmailAddress))
            {
                return ResponseDto<NoDataDto>.Fail("This email address is already registered.", StatusCodes.Status400BadRequest);
            }
            if (_appDbContext.Emails.Any(x => x.EmailAddress == request.EmailAddress))
            {
                var isDeletedEmail = await _appDbContext.Emails.Where(x => x.EmailAddress == request.EmailAddress).FirstOrDefaultAsync();
                isDeletedEmail.IsActive = true;
                await _appDbContext.SaveChangesAsync();
                return ResponseDto<NoDataDto>.Success(StatusCodes.Status204NoContent);
            }

            var email = ObjectMapper.Mapper.Map<Email>(request);
            await _appDbContext.Emails.AddAsync(email);
            await _appDbContext.SaveChangesAsync();
            return ResponseDto<NoDataDto>.Success(StatusCodes.Status204NoContent);
        }
        public async Task<ResponseDto<List<EmailDto>>> GetEmailsListAsync()
        {
            var EmailList = await GetEmails().ToListAsync();
            return ResponseDto<List<EmailDto>>.Success(ObjectMapper.Mapper.Map<List<EmailDto>>(EmailList), StatusCodes.Status200OK);
        }
        public async Task<ResponseDto<NoDataDto>> RemoveEmailAsync(int id)
        {
            var email = await _appDbContext.Emails.FindAsync(id);
            email.IsActive = false;
            await _appDbContext.SaveChangesAsync();
            return ResponseDto<NoDataDto>.Success(StatusCodes.Status204NoContent);
        }
        public async Task<ResponseDto<NoDataDto>> SendEmailAsync(SendEmailDto request)
        {

            var smtpCient = new SmtpClient();
            smtpCient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpCient.UseDefaultCredentials = false;
            smtpCient.Host = _emailSettings.Host;
            smtpCient.Port = _emailSettings.Port;
            smtpCient.Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password);
            smtpCient.EnableSsl = true;

            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(_emailSettings.Email);
            var emails = await GetEmails().Where(x => x.Id == request.EmailId).FirstOrDefaultAsync();
            mailMessage.To.Add(emails.EmailAddress);
            mailMessage.Subject = request.Subject;
            mailMessage.Body = request.Body;

            await smtpCient.SendMailAsync(mailMessage);
            await SaveSentEmail(request);
            return ResponseDto<NoDataDto>.Success(StatusCodes.Status204NoContent);

        }
        public async Task SaveSentEmail(SendEmailDto request)
        {
            var sentEmail = ObjectMapper.Mapper.Map<EmailHistory>(request);
            sentEmail.SenderEmail = _emailSettings.Email;
            await _appDbContext.EmailHistories.AddAsync(sentEmail);
            await _appDbContext.SaveChangesAsync();
        }
        public IQueryable<Email> GetEmails()
        {
            var emails = _appDbContext.Emails.Where(x => x.IsActive == true);
            return emails;
        }
        public bool EmailIdAny(int id)
        {
            return GetEmails().Any(x => x.Id == id);
        }
    }
}