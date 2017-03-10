using Microsoft.AspNetCore.Mvc;

namespace StickerApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public RedirectResult GetHomePage()
        {
            return new RedirectResult("/swagger/");
        }
    }
}