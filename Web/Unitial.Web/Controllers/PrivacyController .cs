using Microsoft.AspNetCore.Mvc;

namespace Unitial.Web.Controllers
{
    public class PrivacyController : Controller
    {
        [HttpGet("/Privacy")]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}