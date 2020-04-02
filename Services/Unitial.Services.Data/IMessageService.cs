namespace Unitial.Services.Data
{
    public interface IMessageService
    {
        public string CreateConversation(string senderId, string receiverId);
        public void DeleteConversation(string senderId, string receiverId);
        public bool IsCreated(string senderId, string receiverId);
    }
}
