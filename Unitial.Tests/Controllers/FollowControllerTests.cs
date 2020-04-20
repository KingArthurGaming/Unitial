using System.Collections.Generic;
using System.Threading.Tasks;
using Unitial.Data.Models;
using Unitial.Services.Data;
using Unitial.Web.Controllers;
using Unitial.Web.ViewModels.Message;
using Xunit;

namespace Unitial.Tests.Controllers
{
    public class FollowControllerTests
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("   ")]
        [InlineData(" ")]
        [InlineData(" ")]
        public void TestGetNewMessageReceiverIdTextError(string FollowedId)
        {
            var controller = new FollowController(new MockFollowService(), new MockUserService());

            var result = controller.FollowUser(FollowedId);

            Assert.Equal("FollowedId can't be null.", result);

        }




        public class MockFollowService : IFollowService
        {
            public void Follow(string follower, string followed)
            {
                throw new System.NotImplementedException();
            }

            public int GetFollowed(string userId)
            {
                throw new System.NotImplementedException();
            }

            public ICollection<string> GetFollowedIds(string userId)
            {
                throw new System.NotImplementedException();
            }

            public int GetFollowers(string userId)
            {
                throw new System.NotImplementedException();
            }

            public string IsFollowed(string follower, string followed)
            {
                throw new System.NotImplementedException();
            }

            public void Unfollow(string follower, string followed)
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
