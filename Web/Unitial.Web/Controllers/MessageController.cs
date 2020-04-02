using Microsoft.AspNetCore.Mvc;

namespace Unitial.Web.Controllers
{
    public class MessageController : Controller
    {
        public MessageController()
        {

        }
        public IActionResult Index()
        {
            var username = User.Identity.Name;
            //var uesrId = userService.GetMyUserIdByUsername(username);
            return View();
        }
    }
}