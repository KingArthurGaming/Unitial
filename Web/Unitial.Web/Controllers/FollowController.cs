using Microsoft.AspNetCore.Mvc;
using Unitial.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Unitial.Web.Controllers
{
    public class FollowController : Controller
    {
        private readonly IFollowService followService;

        public FollowController(IFollowService followService)
        {
            this.followService = followService;
        }
        public string FollowUser(string FollowedId)
        {
            var username = User.Identity.Name;
            var uesrId = followService.GetMyUserIdByUsername(username);
            var isFollowed = followService.IsFollowed(uesrId, FollowedId);
            if (isFollowed == "Followed")
            {
                followService.Unfollow(uesrId, FollowedId);
            }
            else
            {
                followService.Follow(uesrId, FollowedId);
            }
            return isFollowed;
        
    }
}
}