using EmailServiceExample.API.Contracts;
using EmailServiceExample.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EmailServiceExample.API.Filters
{
    public class SendEmailNotFoundFilter : IAsyncActionFilter
    {
        private readonly IEmailService _emailService;

        public SendEmailNotFoundFilter(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var requestObject = context.ActionArguments["request"];
            var request = requestObject as SendEmailDto;
            var anyemail = _emailService.EmailIdAny(request.EmailId);
            if (anyemail)
            {
                await next.Invoke();
            }
            context.Result = new NotFoundObjectResult(ResponseDto<NoDataDto>.Fail("id not found", 404));
            return;
        }
    }
}
