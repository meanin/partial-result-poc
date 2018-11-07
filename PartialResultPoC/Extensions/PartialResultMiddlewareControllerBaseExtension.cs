using Microsoft.AspNetCore.Mvc;

namespace PartialResultPoC.Extensions
{
    public static class PartialResultMiddlewareControllerBaseExtension
    {
        public static void SetPartialSuccess(this ControllerBase controllerBase, bool partialSuccess)
        {
            controllerBase.HttpContext.Items["PartialSuccess"] = partialSuccess;
        }
    }
}