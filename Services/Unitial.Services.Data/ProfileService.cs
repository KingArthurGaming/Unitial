using System.Linq;
using Unitial.Data.Common.Repositories;
using Unitial.Data.Models;
using Unitial.Web.ViewModels;

namespace Unitial.Services.Data
{
    public class ProfileService : IProfileService
    {
        private readonly IRepository<ApplicationUser> userRepository;

        public ProfileService(IRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public string GetMyUserIdByUsername(string username)
        {

            var userId = userRepository
                .All()
                .Where(x => x.UserName == username)
                .Select(x => x.Id)
                .FirstOrDefault();
            return userId;
        }

        public UsersProfileViewModel GetUserInfo(string userId)
        {
            var userInfo = userRepository
                .All()
                .Where(x => x.Id == userId)
                .Select(x => new UsersProfileViewModel()
                {
                    Username = x.UserName,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl
                })
                .FirstOrDefault();
            return userInfo;
        }

        public void EditUserInfo(UserEditInfoModel userInfo, string userId)
        {

            var user = userRepository.All().Where(x => x.Id == userId).FirstOrDefault();

            if (userInfo.ImageUrl != null)
            {
                user.ImageUrl = userInfo.ImageUrl;
            }

            if (userInfo.Description != null)
            {
                user.Description = userInfo.Description;
            }
            user.FirstName = userInfo.FirstName;
            user.LastName = userInfo.LastName;

            userRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}
