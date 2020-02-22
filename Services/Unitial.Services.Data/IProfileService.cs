﻿using Unitial.Web.ViewModels;

namespace Unitial.Services.Data
{
    public interface IProfileService
    {
        public UsersProfileViewModel GetUserInfo(string userId);

        public string GetMyUserIdByUsername(string username);

        public void EditUserInfo(UserEditInfoModel userInfo, string userId);

    }
}