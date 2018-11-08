using Microsoft.AspNetCore.Mvc;

namespace PartialResultPoC2.Extensions
{
    public static class PartialOkExtension
    {
        public static OkObjectResult PartialOk<T>(this ControllerBase controller, T value, bool success)
        {
            return new OkObjectResult(new PartialResponse<T>{Success = success, Body = value});
        }
    }

    public class PartialResponse<T>
    {
        public bool Success { get; set; }
        public T Body { get; set; }
    }
}
