using System.Linq;
using Unitial.Data.Common.Repositories;
using Unitial.Data.Models;

namespace Unitial.Services.Data
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<Conversation> conversationRepository;

        public MessageService(IRepository<Conversation> conversationRepository)
        {
            this.conversationRepository = conversationRepository;
        }

        public void CreateConversation(string senderId, string receiverId)
        {
            var conversation = new Conversation()
            {
                FirstUserId = senderId,
                SecondUserId = receiverId,
                SeenFirstUser = true,
                SeenSecondUser = true,
            };
            conversationRepository.AddAsync(conversation);

            conversationRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public void DeleteConversation(string senderId, string receiverId)
        {
            var conversation = conversationRepository.All().Where(x =>
              (x.FirstUserId == senderId && x.SecondUserId == receiverId) ||
              (x.SecondUserId == senderId && x.FirstUserId == receiverId)).FirstOrDefault();

            conversationRepository.Delete(conversation);

            conversationRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public bool IsCreated(string senderId, string receiverId)
        {
            var IsCreated = conversationRepository.All().Where(x =>
              (x.FirstUserId == senderId && x.SecondUserId == receiverId) ||
              (x.SecondUserId == senderId && x.FirstUserId == receiverId)).Any();

            return IsCreated;
        }
    }
}
