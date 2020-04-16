using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unitial.Services.Data;

namespace Unitial.Web.Controllers
{
    [Authorize]
    public class FollowController : Controller
    {
        private readonly IFollowService followService;
        private readonly IUserService userService;

        public FollowController(IFollowService followService, IUserService userService)
        {
            this.followService = followService;
            this.userService = userService;
        }
        public string FollowUser(string FollowedId)
        {
            var username = User.Identity.Name;
            var uesrId =  userService.GetUserIdByUsername(username).GetAwaiter().GetResult();
            var isFollowed = followService.IsFollowed(uesrId, FollowedId);
            if (isFollowed == "Followed")
            {
                followService.Unfollow(uesrId, FollowedId);
            }
            else
            {
                followService.Follow(uesrId, FollowedId);
            }
            return isFollowed;

        }
    }
}