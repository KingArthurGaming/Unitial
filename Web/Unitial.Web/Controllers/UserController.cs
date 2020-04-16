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

        public UserController(IProfileService profileService, IUserService userService)
        {
            this.profileService = profileService;
            this.userService = userService;
        }

        [Authorize]
        public IActionResult MyProfile()
        {
            var username = User.Identity.Name;
            var uesrId = userService.GetUserIdByUsername(username).GetAwaiter().GetResult();
            return Redirect($"Profile?uesrId={uesrId}");
        }

        [Authorize]
        public IActionResult Profile(string uesrId)
        {
            var username = User.Identity.Name;
            var activeUserId = userService.GetUserIdByUsername(username).GetAwaiter().GetResult();
            var user = profileService.GetUserInfo(uesrId, activeUserId).GetAwaiter().GetResult();
            return View(user);
        }

        [Authorize]
        public IActionResult Edit()
        {
            var username = User.Identity.Name;
            var uesrId = userService.GetUserIdByUsername(username).GetAwaiter().GetResult();
            var user = profileService.GetUserInfo(uesrId, uesrId).GetAwaiter().GetResult();

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
            var uesrId = userService.GetUserIdByUsername(username).GetAwaiter().GetResult();
              profileService.EditUserInfo(userInfo, uesrId).GetAwaiter().GetResult();

            return Redirect($"Profile?uesrId={uesrId}");
        }


    }
}
