using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public LikeController(ILikeService likeService, IPostService postService)
        {
            this.likeService = likeService;
            this.postService = postService;
        }
        [HttpPost]
        public string LikePost(string id)
        {

            var username = User.Identity.Name;
            var uesrId = postService.GetMyUserIdByUsername(username);

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