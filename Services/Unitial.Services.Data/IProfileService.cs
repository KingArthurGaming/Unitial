﻿using Microsoft.AspNetCore.Http;
using Unitial.Web.ViewModels;

namespace Unitial.Services.Data
{
    public interface IProfileService
    {
        public UsersProfileViewModel GetUserInfo(string userId, string activeUserId);
        public void EditUserInfo(UserEditInfoModel userInfo, string userId);



    }
}
