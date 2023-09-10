using EmailServiceExample.API.DTOs;
using EmailServiceExample.API.Model;

namespace EmailServiceExample.API.Contracts
{
    public interface IEmailService
    {
        
        IQueryable<Email> GetEmails();
        Task<ResponseDto<List<EmailDto>>> GetEmailsListAsync();
        Task<ResponseDto<NoDataDto>> AddEmailAsync(CreateEmailDto request);
        Task<ResponseDto<NoDataDto>> RemoveEmailAsync(int id);
        Task<ResponseDto<NoDataDto>> SendEmailAsync(SendEmailDto request);
        Task SaveSentEmail(SendEmailDto request);
        bool EmailIdAny(int id);
    }
}
