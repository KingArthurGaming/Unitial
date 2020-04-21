using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Unitial.Services.Data;
using Unitial.Web.ViewModels;

namespace Unitial.Web.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostService postService;
        private readonly IUserService userService;

        public PostController(IPostService postService, IUserService userService)
        {
            this.postService = postService;
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(CreatePostInputModel createPostInput)
        {
            if (createPostInput.Caption.Length > 65)
            {
                createPostInput.Caption = "";
            }

            var username = User.Identity.Name;
            var uesrId = userService.GetUserIdByUsername(username).GetAwaiter().GetResult();

            postService.CreatePost(createPostInput, uesrId).GetAwaiter().GetResult();

            return Redirect("/User/MyProfile");
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(string id)
        {
            await postService.DeletePost(id);
            return Redirect("/User/MyProfile");
        }




    }
}

