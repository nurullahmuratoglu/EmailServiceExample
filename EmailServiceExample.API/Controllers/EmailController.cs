using EmailServiceExample.API.DTOs;
using EmailServiceExample.API.Filters;
using EmailServiceExample.API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace EmailServiceExample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }
        [HttpGet]
        public async Task<ResponseDto<List<EmailDto>>> GetEmailList()
        {
            return await _emailService.GetEmailsListAsync();
        }
        [ServiceFilter(typeof(ValidateEmailFilter))]
        [HttpPost]
        public async Task<ResponseDto<NoDataDto>> AddEmail([FromForm]CreateEmailDto request)
        {
            return await _emailService.AddEmailAsync(request);
        }
        [ServiceFilter(typeof(EmailNotFoundFilter))]
        [HttpDelete("{id}")]
        public async Task<ResponseDto<NoDataDto>> RemoveEmail(int id)
        { 
            return await _emailService.RemoveEmailAsync(id);
        }
        [ServiceFilter(typeof(SendEmailNotFoundFilter))]
        [Route("SendEmail")]
        [HttpPost]
        public async Task<ResponseDto<NoDataDto>> SendEmail(SendEmailDto request)
        {
            return await _emailService.SendEmailAsync(request);
        }

    }
}
