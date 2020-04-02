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
            var allConversation = messageService.GetAllConversations(uesrId);
            return View(allConversation);
        }
        [HttpGet]
        public IActionResult Index(string receiverId)
        {
            if (receiverId == null)
            {
                return Redirect("/Message/All");

            }

            var username = User.Identity.Name;
            var uesrId = userService.GetUserByUsername(username);
            var conversationId = "";
            if (!messageService.IsCreated(uesrId, receiverId))
            {
                conversationId = messageService.CreateConversation(uesrId, receiverId);

            }
            if (conversationId == null)
            {
                return Redirect("/Message/All");
            }

            return View();
        }
    }
}