using Microsoft.AspNetCore.Mvc;
using Unitial.Services.Data;

namespace Unitial.Web.Controllers
{
    public class MessageController : Controller
    {
        private readonly IConversationService conversiationService;
        private readonly IMessageService messageService;
        private readonly IUserService userService;

        public MessageController(IConversationService conversationService,
            IMessageService messageService,
            IUserService userService)
        {
            this.conversiationService = conversationService;
            this.messageService = messageService;
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
            var chatInfo = messageService.GetAllMessages(uesrId, receiverId);
            return View(chatInfo);
        }

        [HttpPost]
        public string SendMessage(string text, string conversationId)
        {
            if (text == null || text == " " || text == " " || conversationId == null)
            {
                return null;
            }
            var username = User.Identity.Name;
            var uesrId = userService.GetUserByUsername(username);
            messageService.SendMessage(text, conversationId, uesrId);
            return "Go";
        }
        [HttpPost]
        public string GetNewMessage(string lastMessage, string receiverId)
        {
            if (lastMessage == null || lastMessage == " " || receiverId == " " || receiverId == null)
            {
                return null;
            }
            var username = User.Identity.Name;
            var uesrId = userService.GetUserByUsername(username);
            var newMessages = messageService.GetNewMessages(lastMessage, receiverId, uesrId);
            var jsonResponse =  Newtonsoft.Json.JsonConvert.SerializeObject(newMessages);
            return "Go";
        }
    }
}