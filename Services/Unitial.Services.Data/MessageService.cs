using System;
using System.Collections.Generic;
using System.Linq;
using Unitial.Data.Common.Repositories;
using Unitial.Data.Models;
using Unitial.Web.ViewModels.Message;

namespace Unitial.Services.Data
{
    public class MessageService : IMessageService
    {
        private readonly IRepository<Message> messageRepository;
        private readonly IConversationService conversationService;
        private readonly IRepository<ApplicationUser> userRepository;

        public MessageService(
            IRepository<Message> messageRepository,
            IConversationService conversationService,
            IRepository<ApplicationUser> userRepository)
        {
            this.messageRepository = messageRepository;
            this.conversationService = conversationService;
            this.userRepository = userRepository;
        }

        public ICollection<Message> GetNewMessages(string lastMessage, string receiverId, string uesrId)
        {
            var date = DateTime.Parse(lastMessage).AddMilliseconds(999);
            var conversationId = conversationService.GetConversationId(receiverId, uesrId);
            var newMessages = messageRepository.All().Where(x => x.ConversationId == conversationId && x.SenderId == receiverId && x.SendedOn > date).ToList();
            foreach (var item in newMessages)
            {
                item.Conversation = null;
            }
            return newMessages;
        }

        public AllMessagesViewModel GetAllMessages(string senderId, string receiverId)
        {
            var actriveUser = userRepository.All().Where(x => x.Id == senderId).FirstOrDefault();
            var receiverUser = userRepository.All().Where(x => x.Id == receiverId).FirstOrDefault();
            var conversationId = conversationService.GetConversationId(senderId, receiverId);


            var allMessages = new AllMessagesViewModel()
            {
                ActriveUserId = senderId,
                ReceiverUserId = receiverId,
                ActriveUser = actriveUser,
                ReceiverUser = receiverUser,
                ConversationId = conversationId,
                Messages = new List<Message>()
            };
            allMessages.Messages
                .AddRange(messageRepository
                .All()
                .Where(x =>
                (x.SenderId == senderId && x.ConversationId == conversationId) ||
               (x.SenderId == receiverId && x.ConversationId == conversationId)
                ).OrderBy(x => x.SendedOn));
            return allMessages;
        }

        public void SendMessage(string text, string conversationId, string senderId)
        {

            var message = new Message()
            {
                Text = text,
                ConversationId = conversationId,
                SenderId = senderId
            };
            conversationService.SetLastUpdate(conversationId);
            conversationService.MakeIsUnseen(conversationId, senderId);
            messageRepository.AddAsync(message);
            messageRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }


    }
}
