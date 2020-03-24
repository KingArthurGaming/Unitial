using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unitial.Services.Data;

namespace Unitial.Web.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;
        private readonly IProfileService profileService;

        public CommentController(ICommentService commentService,IProfileService profileService)
        {
            this.commentService = commentService;
            this.profileService = profileService;
        }
        public IActionResult CreateComment(string id ,string comment)
        {
            if (comment.Length < 0 && comment.Length > 65)
            {
                return null;
            }
            var username = User.Identity.Name;
            var uesrId = profileService.GetMyUserIdByUsername(username);
            commentService.CreateComment(id, uesrId, comment);
            return View();
        }
    }
}
