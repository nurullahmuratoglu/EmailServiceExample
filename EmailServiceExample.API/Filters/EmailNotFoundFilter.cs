using EmailServiceExample.API.Contracts;
using EmailServiceExample.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EmailServiceExample.API.Filters
{
    public class EmailNotFoundFilter : IAsyncActionFilter
    {
        private readonly IEmailService _emailService;

        public EmailNotFoundFilter(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var id =(int)context.ActionArguments.Values.FirstOrDefault();
            var anyemail =  _emailService.EmailIdAny(id);
            if (anyemail)
            {
                await next.Invoke();
            }
            context.Result = new NotFoundObjectResult(ResponseDto<NoDataDto>.Fail("id not found", 404));
            return;
        }
    }
}
