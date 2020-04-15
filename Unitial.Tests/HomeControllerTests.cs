using System.Collections.Generic;
using Unitial.Services.Data;
using Unitial.Web.Controllers;
using Unitial.Web.ViewModels;
using Xunit;

namespace Unitial.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void TestViewModelForHomePage()
        {
            var controller = new HomeController(new MockPostService(), new MockUserService());
        }



        public class MockPostService : IPostService
        {
            public void CreatePost(CreatePostInputModel createPostInput, string userId)
            {
                throw new System.NotImplementedException();
            }

            public void DeletePost(string postId)
            {
                throw new System.NotImplementedException();
            }

            public ICollection<PostViewModel> GetPostsById(string userId, string activeUserId)
            {
                throw new System.NotImplementedException();
            }
        }
        public class MockUserService : IUserService
        {
            public string GetUserByUsername(string Username)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
