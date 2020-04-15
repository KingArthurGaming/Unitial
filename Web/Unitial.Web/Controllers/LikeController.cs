using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unitial.Services.Data;

namespace Unitial.Web.Controllers
{
    [Authorize]
    public class LikeController : Controller
    {
        private readonly ILikeService likeService;
        private readonly IPostService postService;
        private readonly IUserService userService;

        public LikeController(ILikeService likeService, IPostService postService, IUserService userService)
        {
            this.likeService = likeService;
            this.postService = postService;
            this.userService = userService;
        }
        [HttpPost]
        public string LikePost(string id)
        {

            var username = User.Identity.Name;
            var uesrId = userService.GetUserByUsername(username);

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