using System.Collections.Generic;
using Unitial.Data.Models;
using Unitial.Web.ViewModels.Message;

namespace Unitial.Services.Data
{
    public interface IMessageService
    {
        public AllMessagesViewModel GetAllMessages(string senderId, string receiverId);
        public void SendMessage(string text, string conversationId, string senderId);
        public ICollection<Message> GetNewMessages(string lastMessage, string receiverId, string uesrId);
    }
}
