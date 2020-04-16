using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unitial.Services.Data;
using Unitial.Web.Controllers;
using Unitial.Web.ViewModels;
using Xunit;

namespace Unitial.Tests
{
    public class UserControllerTests
    {
       //[Fact]
        public void TestUserProfileError()
        {
            var controller = new UserController(new MockProfileService(), new MockUserService());
            var result = controller.Profile("");

            Assert.IsType<ViewResult>(result);

        }
        public class MockProfileService : IProfileService
        {
            public Task EditUserInfo(UserEditInfoModel userInfo, string userId)
            {
                throw new System.NotImplementedException();
            }

            public Task<UsersProfileViewModel> GetUserInfo(string userId, string activeUserId)
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
