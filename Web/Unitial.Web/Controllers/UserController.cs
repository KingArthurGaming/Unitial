namespace Unitial.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Unitial.Services.Data;
    using Unitial.Web.ViewModels;

    public class UserController : Controller
    {
        private readonly IProfileService profileService;
        private readonly IUserService userService;

        public UserController(IProfileService profileService ,IUserService userService)
        {
            this.profileService = profileService;
            this.userService = userService;
        }

        [Authorize]
        public IActionResult MyProfile()
        {
            var username = User.Identity.Name;
            var uesrId =  userService.GetUserByUsername(username);
            return Redirect($"Profile?uesrId={uesrId}");
        }

        [Authorize]
        public IActionResult Profile(string uesrId)
        {
            var username = User.Identity.Name;
            var activeUserId =  userService.GetUserByUsername(username);
            var user = profileService.GetUserInfo(uesrId, activeUserId);
            return View(user);
        }

        [Authorize]
        public IActionResult Edit()
        {
            var username = User.Identity.Name;
            var uesrId = userService.GetUserByUsername(username);
            var user = profileService.GetUserInfo(uesrId, uesrId);

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
            var uesrId = userService.GetUserByUsername(username);
            profileService.EditUserInfo(userInfo, uesrId);

            return Redirect($"Profile?uesrId={uesrId}");
        }


    }
}
