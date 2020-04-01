using Microsoft.AspNetCore.Mvc;

namespace Unitial.Web.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}