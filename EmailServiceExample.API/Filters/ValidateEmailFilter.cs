using EmailServiceExample.API.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;

namespace EmailServiceExample.API.Filters
{
    public class ValidateEmailFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var requestObject = context.ActionArguments["request"];
            var request = requestObject as CreateEmailDto;
            var emailAttribute = new EmailAddressAttribute();
            if (!emailAttribute.IsValid(request.EmailAddress))
            {
                context.Result = new BadRequestObjectResult(ResponseDto<NoDataDto>.Fail($"{request.EmailAddress} is not a valid email address.", 400));
                return;
            }
            await next.Invoke();
        }
    }
}

