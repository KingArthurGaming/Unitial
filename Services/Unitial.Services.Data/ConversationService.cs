using System;
using System.Collections.Generic;
using System.Linq;
using Unitial.Data.Common.Repositories;
using Unitial.Data.Models;
using Unitial.Web.ViewModels.Message;

namespace Unitial.Services.Data
{
    public class ConversationService : IConversationService
    {
        private readonly IRepository<Conversation> conversationRepository;
        private readonly IRepository<Message> messageRepository;
        private readonly IRepository<ApplicationUser> userRepository;

        public ConversationService(
            IRepository<Conversation> conversationRepository,
            IRepository<Message> messageRepository,
            IRepository<ApplicationUser> userRepository)
        {
            this.conversationRepository = conversationRepository;
            this.messageRepository = messageRepository;
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
        public string GetConversationId(string senderId, string receiverId)
        {

            var id = conversationRepository.All().Where(x =>
           (x.FirstUserId == senderId && x.SecondUserId == receiverId) ||
           (x.SecondUserId == senderId && x.FirstUserId == receiverId)).Select(x => x.Id).FirstOrDefault();

            conversationRepository.SaveChangesAsync().GetAwaiter().GetResult();

            return id;
        }
        public void DeleteConversation(string senderId, string receiverId)
        {
            var conversation = conversationRepository.All().Where(x =>
              (x.FirstUserId == senderId && x.SecondUserId == receiverId) ||
              (x.SecondUserId == senderId && x.FirstUserId == receiverId)).FirstOrDefault();

            conversationRepository.Delete(conversation);

            conversationRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public AllConversationsViewModel GetAllConversations(string userId)
        {
            var conversation = conversationRepository
                .All()
                .Where(x => x.SecondUserId == userId || x.FirstUserId == userId)
                .Select(x => new ConversationViewModel()
                {
                    LastUpdate = x.LastUpdate,
                    IsSeen = userId == x.FirstUserId ? x.SeenFirstUser : x.SeenSecondUser,
                    User = userRepository.All().Where(y => y.Id == (x.SecondUserId == userId ? x.FirstUserId : x.SecondUserId)).FirstOrDefault()
                }).OrderByDescending(x => x.LastUpdate);
            var allConversations = new AllConversationsViewModel() { Conversations = conversation };
            return allConversations;
        }


        public bool IsCreated(string senderId, string receiverId)
        {
            var IsCreated = conversationRepository.All().Where(x =>
              (x.FirstUserId == senderId && x.SecondUserId == receiverId) ||
              (x.SecondUserId == senderId && x.FirstUserId == receiverId)).Any();

            return IsCreated;
        }

        public void SetLastUpdate(string conversationId)
        {
            conversationRepository.All().Where(x => x.Id == conversationId).FirstOrDefault().LastUpdate = DateTime.UtcNow;
            conversationRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public void MakeIsUnseen(string conversationId, string senderId)
        {
            var conversation = conversationRepository.All().Where(x => x.Id == conversationId).FirstOrDefault();
            if (conversation.FirstUserId == senderId)
            {
                conversation.SeenSecondUser = false;
                conversation.SeenFirstUser = true;
            }
            else
            {
                conversation.SeenFirstUser = false;
                conversation.SeenSecondUser = true;
            }
            conversationRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public void Seen(string conversationId, string userId)
        {
            var conversation = conversationRepository.All().Where(x => x.Id == conversationId).FirstOrDefault();
            if (conversation.FirstUserId == userId)
            {
                conversation.SeenFirstUser = true;
            }
            else
            {
                conversation.SeenSecondUser = true;
            }
            conversationRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}
