using Microsoft.AspNetCore.Mvc;

namespace EmailServiceExample.API.DTOs
{
    public class ResponseDto<T>
    {
        public T Data { get; set; }
        public List<string> Errors { get; set; }
        public int StatusCode { get; set; }

        public static ResponseDto<T> Success(T data, int statusCode)
        {
            return new ResponseDto<T> { Data = data, StatusCode = statusCode };
        }
        public static ResponseDto<T> Success(int statusCode)
        {
            return new ResponseDto<T> { StatusCode = statusCode };
        }
        public static ResponseDto<T> Fail(List<string> errors, int statusCode)
        {
            return new ResponseDto<T> { Errors = errors, StatusCode = statusCode };
        }
        public static ResponseDto<T> Fail(string error, int statusCode)
        {
            return new ResponseDto<T> { Errors = new List<string> { error }, StatusCode = statusCode };
        }
        public static NoContentResult NoContentStatusCode()
        {
            return new NoContentResult();
        }
    }

}
