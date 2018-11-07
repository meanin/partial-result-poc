using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;

namespace PartialResultPoC.Middlewares
{
    public class PartialResultMiddleware
    {
        private readonly RequestDelegate _next;
        private const string PartialSuccessKey = "PartialSuccess";

        public PartialResultMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Items[PartialSuccessKey] = true;
            var originalBody = context.Response.Body;
            try
            {
                using (var memStream = new MemoryStream())
                {
                    context.Response.Body = memStream;
                    await _next(context);
                    memStream.Position = 0;
                    await memStream.CopyToAsync(originalBody);
                    if (context.Response.StatusCode != StatusCodes.Status200OK)
                    {
                        context.Response.Body = originalBody;
                        return;
                    }

                    memStream.Position = 0;
                    var streamContent = new StreamReader(memStream).ReadToEnd();

                    var token = JToken.Parse(streamContent);
                    var partialSuccess = (bool)context.Items[PartialSuccessKey];
                    using (var streamWriter = new StreamWriter(originalBody))
                    {
                        originalBody.Position = 0;
                        streamWriter.Write(new { Success = partialSuccess, Body = token });
                    }
                }
            }
            finally
            {
                context.Response.Body = originalBody;
            }
        }
    }
}
