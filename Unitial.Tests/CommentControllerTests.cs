using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Unitial.Services.Data;
using Unitial.Web.Controllers;
using Unitial.Web.ViewModels;
using Xunit;

namespace Unitial.Tests
{
    public class CommentControllerTests
    {
        [Theory]
        [InlineData("Empty" ,"")]
        [InlineData("Null" ,null)]
        [InlineData("Mixed", "   ")]
        [InlineData("Space", " ")]
        [InlineData("Alt+0160", " ")]
        [InlineData("Too Long", "123456797123456797123456797123456797123456797123456797123456797555")]
        public void TestCreateCommenetCommentTextError(string id, string comment)
        {
            var controller = new CommentController(new MockCommentService(), new MockUserService());

            var result = controller.CreateComment(id, comment);

            Assert.Equal("Comment need to be between 1 and 65.", result);

        }

        [Theory]
        [InlineData("", "Empty")]
        [InlineData(null, "Null")]
        [InlineData("  ", "Mixed")]
        [InlineData(" ", "Space")]
        [InlineData(" ", "Alt+0160")]
        public void TestCreateCommentIdTextError(string id, string comment)
        {
            var controller = new CommentController(new MockCommentService(), new MockUserService());

            var result = controller.CreateComment(id, comment);

            Assert.Equal("Id can't be null or empty.", result);

        }

        //"Comment need to be between 0 and 65.";
        //"Id can't be null.";
        //"User doesn't exist.";
        public class MockCommentService : ICommentService
        {
            public void CreateComment(string postId, string authorId, string text)
            {
                
            }

            public ICollection<CommetViewModel> GetComments(string postId)
            {
                throw new System.NotImplementedException();
            }
        }
        public class MockUserService : IUserService
        {
            public string GetUserIdByUsername(string Username)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
