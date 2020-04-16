using Unitial.Web.ViewModels.Message;

namespace Unitial.Services.Data
{
    public interface IConversationService
    {
        public string CreateConversation(string senderId, string receiverId);
        public void DeleteConversation(string senderId, string receiverId);
        public bool IsCreated(string senderId, string receiverId);
        public AllConversationsViewModel GetAllConversations(string senderId);
        public string GetConversationId(string senderId, string receiverId);
        public void SetLastUpdate(string conversationId);
        public void MakeIsUnseen(string conversationId, string senderId);
        public void Seen(string conversationId, string userId);
        public bool CheckForNew(string userId);
    }
}
