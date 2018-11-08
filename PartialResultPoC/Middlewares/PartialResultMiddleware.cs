using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

            using (var memStream = new MemoryStream())
            {
                try
                {
                    context.Response.Body = memStream;
                    await _next(context);
                    memStream.Position = 0;

                    if (context.Response.StatusCode != StatusCodes.Status200OK)
                    {
                        await memStream.CopyToAsync(originalBody);
                        context.Response.Body = originalBody;
                        return;
                    }

                    var streamContent = new StreamReader(memStream).ReadToEnd();
                    var token = JToken.Parse(streamContent);
                    var partialSuccess = (bool)context.Items[PartialSuccessKey];
                    using (var streamWriter = new StreamWriter(originalBody))
                    {
                        var content = JsonConvert.SerializeObject(new {Success = partialSuccess, Body = token});
                        streamWriter.Write(content);
                    }
                    context.Response.Body = originalBody;
                }
                catch
                {
                    memStream.Position = 0;
                    await memStream.CopyToAsync(originalBody);
                    context.Response.Body = originalBody;
                }
            }
        }
    }
    public static class PartialResultMiddlewareControllerBaseExtension
    {
        public static void SetPartialSuccess(this ControllerBase controllerBase, bool partialSuccess)
        {
            controllerBase.HttpContext.Items["PartialSuccess"] = partialSuccess;
        }
    }
}
