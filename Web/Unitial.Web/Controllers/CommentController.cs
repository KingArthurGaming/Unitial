using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Unitial.Services.Data;

namespace Unitial.Web.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentService commentService;
        private readonly IUserService userService;

        public CommentController(ICommentService commentService, IUserService userService)
        {
            this.commentService = commentService;
            this.userService = userService;
        }
        public async Task<string> CreateComment(string id, string comment)
        {
            if (comment == null || comment.Replace(" ", "").Replace(" ", "").Length <= 0 || comment.Length >= 65)
            {
                return "Comment need to be between 1 and 65.";
            }
            if (id == null || id.Replace(" ", "").Replace(" ", "").Length <= 0)
            {
                return "Id can't be null or empty.";
            }
            var username = User.Identity.Name;
            var uesrId = userService.GetUserIdByUsername(username).GetAwaiter().GetResult();
             commentService.CreateComment(id, uesrId, comment).GetAwaiter().GetResult();
            return "Created";
        }
    }
}
