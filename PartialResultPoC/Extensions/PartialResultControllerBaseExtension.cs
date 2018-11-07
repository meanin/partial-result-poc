using Microsoft.AspNetCore.Mvc;

namespace PartialResultPoC.Extensions
{
    public static class PartialResultControllerBaseExtension
    {
        public class PartialObjectResult
        {
            public bool Success { get; set; }
            public object Body { get; set; }
        }

        public static OkObjectResult PartialResult(this ControllerBase controllerBase, object value, bool success = true)
        {
            var partialObjectResult = new PartialObjectResult
            {
                Success = success,
                Body = value
            };
            return new OkObjectResult(partialObjectResult);
        }
    }
}