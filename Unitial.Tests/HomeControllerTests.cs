using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unitial.Services.Data;
using Unitial.Web.Controllers;
using Unitial.Web.ViewModels;
using Xunit;

namespace Unitial.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void TestViewForErrorPage()
        {
            var controller = new HomeController(new MockPostService(), new MockUserService());
            var result = controller.HttpError(404);
            Assert.IsType<ViewResult>(result);
        }



        public class MockPostService : IPostService
        {
            public Task CreatePost(CreatePostInputModel createPostInput, string userId)
            {
                throw new System.NotImplementedException();
            }

            public Task DeletePost(string postId)
            {
                throw new System.NotImplementedException();
            }

            public Task<ICollection<PostViewModel>> GetPostsById(string userId, string activeUserId)
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
