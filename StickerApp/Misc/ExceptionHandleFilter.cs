using System;
using System.Net;
using System.Threading;
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
            Console.Write("ExceptionHandleFilter.");
        }

        public void OnException(ExceptionContext context)
        {
            Console.Write("On Exception.");
            var code = HttpStatusCode.InternalServerError;
            var response = new ErrorResponse("InternalServerError");

            var appException = context.Exception as StickerAppException;
            if (appException != null)
            {
                code = HttpStatusCode.BadRequest;
                response.Error = appException.Error;
                response.Reason = appException.Reason;
            }

            var responseJson = JsonConvert.SerializeObject(response);
            context.Result = new JsonResult(responseJson);
            context.HttpContext.Response.StatusCode = (int) code;
            context.ExceptionHandled = true;
        }
    }

}