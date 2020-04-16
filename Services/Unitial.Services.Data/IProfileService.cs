using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Unitial.Web.ViewModels;

namespace Unitial.Services.Data
{
    public interface IProfileService
    {
        public Task<UsersProfileViewModel> GetUserInfo(string userId, string activeUserId);
        public Task EditUserInfo(UserEditInfoModel userInfo, string userId);



    }
}
