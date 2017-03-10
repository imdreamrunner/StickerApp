using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StickerApp.Misc;

namespace StickerApp.Controllers
{
    public class AppController : Controller
    {
        protected StickerAppException GenerateModelStateException(string exceptionName)
        {
            var errors = string.Join(" ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));
            return new StickerAppException(exceptionName, errors);
        }
    }
}