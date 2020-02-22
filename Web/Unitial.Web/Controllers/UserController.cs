namespace Unitial.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Unitial.Services.Data;
    using Unitial.Web.ViewModels;

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
            return Redirect($"Profile?uesrId={uesrId}");
        }

        [Authorize]
        public IActionResult Profile(string uesrId)
        {
            var user = profileService.GetUserInfo(uesrId);
            return View(user);
        }

        [Authorize]
        public IActionResult Edit()
        {
            var username = User.Identity.Name;
            var uesrId = profileService.GetMyUserIdByUsername(username);
            var user = profileService.GetUserInfo(uesrId);

            return View(user);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(UserEditInfoModel userInfo)
        {

            if (ModelState.IsValid)
            {
                return View(userInfo);
            }
            var username = User.Identity.Name;
            var uesrId = profileService.GetMyUserIdByUsername(username);
            profileService.EditUserInfo(userInfo, uesrId);

            return Redirect($"Profile?uesrId={uesrId}");
        }


    }
}
