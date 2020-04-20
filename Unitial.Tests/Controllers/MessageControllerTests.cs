using System.Collections.Generic;
using System.Threading.Tasks;
using Unitial.Data.Models;
using Unitial.Services.Data;
using Unitial.Web.Controllers;
using Unitial.Web.ViewModels.Message;
using Xunit;

namespace Unitial.Tests.Controllers
{
    public class MessageControllerTests
    {
        [Theory]
        [InlineData("Empty", "")]
        [InlineData("Null", null)]
        [InlineData("Mixed", "   ")]
        [InlineData("Space", " ")]
        [InlineData("Alt+0160", " ")]
        public void TestGetNewMessageReceiverIdTextError(string LastMessage, string ReceiverId)
        {
            var controller = new MessageController(new MockConversationService(), new MockMessageService(), new MockUserService());

            var result = controller.GetNewMessage(LastMessage, ReceiverId);

            Assert.Equal("ReceiverId can't be null.", result);

        }

        [Theory]
        [InlineData("", "Empty")]
        [InlineData(null, "Null")]
        [InlineData("  ", "Mixed")]
        [InlineData(" ", "Space")]
        [InlineData(" ", "Alt+0160")]
        public void TestGetNewMessageMessageTextError(string LastMessage, string ReceiverId)
        {
            var controller = new MessageController(new MockConversationService(), new MockMessageService(), new MockUserService());

            var result = controller.GetNewMessage(LastMessage, ReceiverId);

            Assert.Equal("Message incorrect.", result);

        }

        //       "Message incorrect."
        //"ReceiverId can't be null."
        [Theory]
        [InlineData("Empty", "")]
        [InlineData("Null", null)]
        [InlineData("Mixed", "   ")]
        [InlineData("Space", " ")]
        [InlineData("Alt+0160", " ")]
        public void TestSendMessageConversationIdTextError(string LastMessage, string ReceiverId)
        {
            var controller = new MessageController(new MockConversationService(), new MockMessageService(), new MockUserService());

            var result = controller.SendMessage(LastMessage, ReceiverId);

            Assert.Equal("ConversationId can't be null", result);

        }

        [Theory]
        [InlineData("", "Empty")]
        [InlineData(null, "Null")]
        [InlineData("  ", "Mixed")]
        [InlineData(" ", "Space")]
        [InlineData(" ", "Alt+0160")]
        public void TestSendMessageMessageTextError(string LastMessage, string ReceiverId)
        {
            var controller = new MessageController(new MockConversationService(), new MockMessageService(), new MockUserService());

            var result = controller.SendMessage(LastMessage, ReceiverId);

            Assert.Equal("Message need to be at least 1 char.", result);

        }

        [Fact]
        public void TestMessageIndexPage()
        {
            var controller = new MessageController(new MockConversationService(), new MockMessageService(), new MockUserService());

            var result = controller.Index(null);
            var resultJson = result.ToString();
            Assert.Equal("Microsoft.AspNetCore.Mvc.RedirectResult", resultJson);

        }


        public class MockConversationService : IConversationService
        {
            public bool CheckForNew(string userId)
            {
                throw new System.NotImplementedException();
            }

            public string CreateConversation(string senderId, string receiverId)
            {
                throw new System.NotImplementedException();
            }

            public void DeleteConversation(string senderId, string receiverId)
            {
                throw new System.NotImplementedException();
            }

            public AllConversationsViewModel GetAllConversations(string senderId)
            {
                throw new System.NotImplementedException();
            }

            public string GetConversationId(string senderId, string receiverId)
            {
                throw new System.NotImplementedException();
            }

            public bool IsCreated(string senderId, string receiverId)
            {
                throw new System.NotImplementedException();
            }

            public void MakeIsUnseen(string conversationId, string senderId)
            {
                throw new System.NotImplementedException();
            }

            public void Seen(string conversationId, string userId)
            {
                throw new System.NotImplementedException();
            }

            public void SetLastUpdate(string conversationId)
            {
                throw new System.NotImplementedException();
            }
        }
        public class MockMessageService : IMessageService
        {
            public AllMessagesViewModel GetAllMessages(string senderId, string receiverId)
            {
                throw new System.NotImplementedException();
            }

            public ICollection<Message> GetNewMessages(string lastMessage, string receiverId, string uesrId)
            {
                throw new System.NotImplementedException();
            }

            public void SendMessage(string text, string conversationId, string senderId)
            {
                throw new System.NotImplementedException();
            }
        }
        public class MockUserService : IUserService
        {
            public Task<string> GetUserIdByUsername(string Username)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
