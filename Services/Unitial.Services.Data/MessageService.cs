using System.Linq;
using Unitial.Data.Common.Repositories;
using Unitial.Data.Models;

namespace Unitial.Services.Data
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<Conversation> conversationRepository;
        private readonly IRepository<ApplicationUser> userRepository;

        public MessageService(IRepository<Conversation> conversationRepository, IRepository<ApplicationUser> userRepository)
        {
            this.conversationRepository = conversationRepository;
            this.userRepository = userRepository;
        }

        public string CreateConversation(string senderId, string receiverId)
        {
            var firstUserExist = userRepository.All().Where(x => x.Id == senderId).Any();
            var secondUserExist = userRepository.All().Where(x => x.Id == receiverId).Any();
            if (firstUserExist && secondUserExist)
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
                return conversation.Id;
            }
            return null;
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
