using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unitial.Services.Data;
using Unitial.Web.ViewModels;

namespace Unitial.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IPostService postService;
        public PostController(IPostService postService)
        {
            this.postService = postService;
            
        }

        [HttpPost]
        public IActionResult CreatePost(CreatePostInputModel createPostInput )
        {
            var username = User.Identity.Name;
            var uesrId = postService.GetMyUserIdByUsername(username);

            postService.CreatePost(createPostInput, uesrId);

            return Redirect("/User/MyProfile");
        }

        

    }
}

