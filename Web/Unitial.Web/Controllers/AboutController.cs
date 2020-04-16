using Microsoft.AspNetCore.Mvc;

namespace Unitial.Web.Controllers
{
    public class AboutController : Controller
    {
        [HttpGet("/About")]
        public IActionResult About()
        {
            return View();
        }
    }
}