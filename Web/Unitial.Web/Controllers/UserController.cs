namespace Unitial.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    public class UserController : Controller
    {
       
        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }


    }
}
