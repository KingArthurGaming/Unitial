using System.Threading.Tasks;
using Unitial.Services.Data;
using Unitial.Web.Controllers;
using Xunit;

namespace Unitial.Tests.Controllers
{
    public class LikeControllerTests
    {


        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("  ")]
        [InlineData(" ")]
        [InlineData(" ")]
        public void TestLikePostIdTextError(string id)
        {
            var controller = new LikeController(new MockLikeService(), new MockUserService());

            var result = controller.LikePost(id);

            Assert.Equal("Id can't be null or empty.", result);

        }

        public class MockLikeService : ILikeService
        {
            public void DislikePost(string postId, string userId)
            {
                throw new System.NotImplementedException();
            }

            public string IsLiked(string postId, string userId)
            {
                throw new System.NotImplementedException();
            }

            public void LikePost(string postId, string userId)
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
