using System;
using System.Net;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using StickerApp.ApiModels;

namespace StickerApp.Misc
{
    public class ExceptionHandleFilter : IExceptionFilter
    {
        public ExceptionHandleFilter()
        {
        }

        public void OnException(ExceptionContext context)
        {
            var code = HttpStatusCode.InternalServerError;
            var response = new ErrorResponse("InternalServerError");

            var appException = context.Exception as StickerAppException;
            if (appException != null)
            {
                code = HttpStatusCode.BadRequest;
                response.Error = appException.Error;
                response.Reason = appException.Reason;
            }

            context.HttpContext.Response.StatusCode = (int) code;
            context.Result = new JsonResult(response);
            // context.ExceptionHandled = true;
            // Cannot set ExceptionHandled = true, otherwise the response content is empty.
            // Check http://stackoverflow.com/questions/5205325/how-to-return-json-result-from-a-custom-exception-filter
        }
    }

}