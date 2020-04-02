using Microsoft.AspNetCore.Mvc;
using Unitial.Services.Data;

namespace Unitial.Web.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService messageService;
        private readonly IUserService userService;

        public MessageController(IMessageService messageService, IUserService userService)
        {
            this.messageService = messageService;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult All()
        {
            var username = User.Identity.Name;
            var uesrId = userService.GetUserByUsername(username);
            return View();
        }
        [HttpGet]
        public IActionResult Index(string receiverId)
        {
            var username = User.Identity.Name;
            var uesrId = userService.GetUserByUsername(username);
            if (!messageService.IsCreated(uesrId, receiverId))
            {
                messageService.CreateConversation(uesrId, receiverId);
            }
            else
            {
                messageService.DeleteConversation(uesrId, receiverId);

            }
            return View();
        }
    }
}