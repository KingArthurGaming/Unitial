namespace Unitial.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Unitial.Services.Data;

    public class UserController : Controller
    {
        private readonly IProfileService profileService;

        public UserController(IProfileService profileService)
        {
            this.profileService = profileService;
        }

        [Authorize]
        public IActionResult MyProfile()
        {
            var username = User.Identity.Name;
            var uesrId = profileService.GetMyUserIdByUsername(username);
            return Redirect($"Profile?uesrId={uesrId}") ;
        }

        [Authorize]
        public IActionResult Profile(string uesrId)
        {
            var user = profileService.GetUserInfo(uesrId);
            return View(user);
        }


    }
}
