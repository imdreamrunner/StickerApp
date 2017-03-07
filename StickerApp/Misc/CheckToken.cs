using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using StickerApp.ApiModels;

namespace StickerApp.Misc
{
    public class CheckTokenAttribute : ServiceFilterAttribute
    {
        public CheckTokenAttribute() : base(typeof(TokenCheckingFilterAttribute))
        {
        }
    }

    public class TokenCheckingFilterAttribute : ActionFilterAttribute
    {
        private readonly ApplicationConfiguration _config;


        public TokenCheckingFilterAttribute(IOptions<ApplicationConfiguration> applicationConfigiration)
        {
            _config = applicationConfigiration.Value;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string token = null;
            if (context.HttpContext.Request.Headers.ContainsKey("token"))
            {
                token = context.HttpContext.Request.Headers["token"];
            }
            if (token == _config.ApiToken) return;
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            context.Result = new JsonResult(new ErrorResponse("Unauthorized"));
        }
    }
}