namespace Unitial.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using Unitial.Web.Services;

    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [Authorize]
        public IActionResult Profile()
        {
            var username = User.Identity.Name;
            var user = userService.GetUserByUsername(username);
            return View(user);
        }


    }
}

