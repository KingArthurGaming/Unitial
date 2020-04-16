using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unitial.Services.Data;

namespace Unitial.Web.Controllers
{
    [Authorize]
    public class LikeController : Controller
    {
        private readonly ILikeService likeService;
        private readonly IUserService userService;

        public LikeController(ILikeService likeService, IUserService userService)
        {
            this.likeService = likeService;
            this.userService = userService;
        }
        [HttpPost]
        public string LikePost(string id)
        {
            if (id == null || id.Replace(" ", "").Replace(" ", "").Length <= 0)
            {
                return "Id can't be null or empty.";
            }

            var username = User.Identity.Name;
            var uesrId = userService.GetUserIdByUsername(username).GetAwaiter().GetResult();

            var isLiked = likeService.IsLiked(id, uesrId);

            if (isLiked == "No")
            {
                likeService.LikePost(id, uesrId);

            }
            else
            {
                likeService.DislikePost(id, uesrId);

            }
            return isLiked;
        }

    }
}