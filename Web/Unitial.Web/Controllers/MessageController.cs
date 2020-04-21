using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Unitial.Services.Data;

namespace Unitial.Web.Controllers
{
    [Authorize]
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
            var uesrId = userService.GetUserIdByUsername(username).GetAwaiter().GetResult();
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
            var uesrId = userService.GetUserIdByUsername(username).GetAwaiter().GetResult();
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
            if (conversationId == null || conversationId.Replace(" ", "").Replace(" ", "").Length <= 0)
            {
                return "ConversationId can't be null";
            }
            if (text == null || text.Replace(" ", "").Replace(" ", "").Length <= 0)
            {
                return "Message need to be at least 1 char.";

            }
            var username = User.Identity.Name;
            var uesrId = userService.GetUserIdByUsername(username).GetAwaiter().GetResult();
            messageService.SendMessage(text, conversationId, uesrId);
            return "Go";
        }


        [HttpPost]
        public string GetNewMessage(string LastMessage, string ReceiverId)
        {
            if (LastMessage == null || LastMessage.Replace(" ", "").Replace(" ", "").Length <= 0)
            {
                return "Message incorrect.";
            }
            if (ReceiverId == null || ReceiverId.Replace(" ", "").Replace(" ", "").Length <= 0)
            {
                return "ReceiverId can't be null.";

            }
            var username = User.Identity.Name;
            var uesrId = userService.GetUserIdByUsername(username).GetAwaiter().GetResult();
            var conversationId = conversiationService.GetConversationId(ReceiverId, uesrId);
            conversiationService.Seen(conversationId, uesrId);
            var newMessages = messageService.GetNewMessages(LastMessage, ReceiverId, uesrId);
            var jsonResponse = Newtonsoft.Json.JsonConvert.SerializeObject(newMessages);
            return jsonResponse;
        }

        [HttpPost]
        public bool CheckForNew()
        {
            var username = User.Identity.Name;
            var uesrId = userService.GetUserIdByUsername(username).GetAwaiter().GetResult();

            var newMessages = conversiationService.CheckForNew(uesrId);
            return newMessages;
        }
    }
}