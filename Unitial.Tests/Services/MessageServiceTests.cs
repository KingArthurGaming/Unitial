using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unitial.Data;
using Unitial.Data.Models;
using Unitial.Data.Repositories;
using Unitial.Services.Data;
using Unitial.Web.ViewModels;
using Unitial.Web.ViewModels.Message;
using Xunit;

namespace Unitial.Tests.Services
{
    public class MessageServiceTests
    {
        [Fact]
        public async Task TestMessageServiceSendMessage()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var userRepo = new EfRepository<ApplicationUser>(
                new ApplicationDbContext(options.Options));
            var messageRepo = new EfRepository<Message>(
                new ApplicationDbContext(options.Options));
            var sender = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
            };
            var senderId = sender.Id;
            var receiver = new ApplicationUser()
            {
                FirstName = "Kiro",
                LastName = "Kirov",
                UserName = "kiro@abv.bg",
                Birthday = DateTime.UtcNow,
            };
            var receiverId = receiver.Id;

            await userRepo.AddAsync(sender);
            await userRepo.AddAsync(receiver);
            await userRepo.SaveChangesAsync();

            var messageService = new MessageService(messageRepo, new MockConversationService(), userRepo);
            messageService.SendMessage("text", "123", senderId);

            var result = await messageRepo.All().CountAsync();
            Assert.Equal(1, result);
            var message = await messageRepo.All().FirstOrDefaultAsync();
            Assert.Equal("123", message.ConversationId);
            Assert.Equal(senderId, message.SenderId);
            Assert.Equal("text", message.Text);


        }

        [Fact]
        public async Task TestMessageServiceGetAllMessages()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var userRepo = new EfRepository<ApplicationUser>(
                new ApplicationDbContext(options.Options));
            var messageRepo = new EfRepository<Message>(
                new ApplicationDbContext(options.Options));
            var sender = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
            };
            var senderId = sender.Id;
            var receiver = new ApplicationUser()
            {
                FirstName = "Kiro",
                LastName = "Kirov",
                UserName = "kiro@abv.bg",
                Birthday = DateTime.UtcNow,
            };
            var receiverId = receiver.Id;

            await userRepo.AddAsync(sender);
            await userRepo.AddAsync(receiver);
            await userRepo.SaveChangesAsync();

            var messageService = new MessageService(messageRepo, new MockConversationService(), userRepo);
            messageService.SendMessage("text1", "123", senderId);
            messageService.SendMessage("text2", "123", receiverId);
            messageService.SendMessage("text3", "123", senderId);

            var result =  messageService.GetAllMessages(senderId, receiverId).Messages;
            Assert.Equal(3, result.Count);
            Assert.Equal("text1", result[0].Text);
            Assert.Equal("text2", result[1].Text);
            Assert.Equal("text3", result[2].Text);
        }

        [Fact]
        public async Task TestMessageServiceGetNewMessages()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var userRepo = new EfRepository<ApplicationUser>(
                new ApplicationDbContext(options.Options));
            var messageRepo = new EfRepository<Message>(
                new ApplicationDbContext(options.Options));
            var sender = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
            };
            var senderId = sender.Id;
            var receiver = new ApplicationUser()
            {
                FirstName = "Kiro",
                LastName = "Kirov",
                UserName = "kiro@abv.bg",
                Birthday = DateTime.UtcNow,
            };
            var receiverId = receiver.Id;

            await userRepo.AddAsync(sender);
            await userRepo.AddAsync(receiver);
            await userRepo.SaveChangesAsync();

            var messageService = new MessageService(messageRepo, new MockConversationService(), userRepo);
            messageService.SendMessage("text1", "123", senderId);
            messageService.SendMessage("text3", "123", receiverId);
            messageService.SendMessage("text3", "123", senderId);
            messageService.SendMessage("text3", "123", senderId);
            messageService.SendMessage("text3", "123", receiverId);

            var lastMessage = DateTime.UtcNow.AddDays(-1).ToString();
            var result = messageService.GetNewMessages(lastMessage, senderId, receiverId);
            Assert.Equal(3, result.Count);
            



        }




    }

    public class MockConversationService : IConversationService
    {
        public bool CheckForNew(string userId)
        {
            throw new NotImplementedException();
        }

        public string CreateConversation(string senderId, string receiverId)
        {
            throw new NotImplementedException();
        }

        public void DeleteConversation(string senderId, string receiverId)
        {
            throw new NotImplementedException();
        }

        public AllConversationsViewModel GetAllConversations(string senderId)
        {
            throw new NotImplementedException();
        }

        public string GetConversationId(string senderId, string receiverId)
        {
            return "123";
        }

        public bool IsCreated(string senderId, string receiverId)
        {
            throw new NotImplementedException();
        }

        public void MakeIsUnseen(string conversationId, string senderId)
        {
            return;
        }

        public void Seen(string conversationId, string userId)
        {
            throw new NotImplementedException();
        }

        public void SetLastUpdate(string conversationId)
        {
            return;
        }
    }
}