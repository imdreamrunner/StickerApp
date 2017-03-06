using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using StickerApp.Responses;

namespace StickerApp.Services
{
    public class TokenCheckingFilter : ActionFilterAttribute
    {
        private readonly ApplicationConfiguration _config;


        public TokenCheckingFilter(IOptions<ApplicationConfiguration> applicationConfigiration)
        {
            _config = applicationConfigiration.Value;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string token = null;
            if (context.HttpContext.Request.Query.ContainsKey("token"))
            {
                token = context.HttpContext.Request.Query["token"];
            }
            else if (context.HttpContext.Request.Form.ContainsKey("token"))
            {
                token = context.HttpContext.Request.Form["token"];
            }
            else if (context.HttpContext.Request.Headers.ContainsKey("token"))
            {
                token = context.HttpContext.Request.Headers["token"];
            }
            if (token == _config.ApiToken) return;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Result = new JsonResult(new ErrorResponse("Unauthorized"));
        }
    }
}