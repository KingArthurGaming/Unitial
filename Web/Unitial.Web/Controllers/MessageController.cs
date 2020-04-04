using Microsoft.AspNetCore.Mvc;
using Unitial.Services.Data;
using Unitial.Web.ViewModels.Message;

namespace Unitial.Web.Controllers
{
    public class MessageController : Controller
    {
        private readonly IConversationService conversiationService;
        private readonly IUserService userService;

        public MessageController(IConversationService conversationService, IUserService userService)
        {
            this.conversiationService = conversationService;
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult All()
        {
            var username = User.Identity.Name;
            var uesrId = userService.GetUserByUsername(username);
            var allConversation = conversiationService.GetAllConversations(uesrId);
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
            if (!conversiationService.IsCreated(uesrId, receiverId))
            {
                conversationId = conversiationService.CreateConversation(uesrId, receiverId);

            }
            if (conversationId == null)
            {
                return Redirect("/Message/All");
            }
            var chatInfo = conversiationService.GetAllMessages(uesrId, receiverId);
            return View(chatInfo);
        }

        [HttpPost]
        public IActionResult SendMessage(MessageInputModel messageInput)
        { 
          
        }
    }
}