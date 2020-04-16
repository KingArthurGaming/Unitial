using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unitial.Data.Common.Repositories;
using Unitial.Data.Models;
using Unitial.Web.ViewModels;

namespace Unitial.Services.Data
{
    public class ProfileService : IProfileService
    {
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IRepository<Post> postRepository;
        private readonly IPostService postService;
        private readonly IFollowService followService;

        public ProfileService(
            IRepository<ApplicationUser> userRepository,
            IRepository<Post> postRepository,
            IPostService postService,
            IFollowService FollowService
            )
        {
            this.userRepository = userRepository;
            this.postRepository = postRepository;
            this.postService = postService;
            followService = FollowService;
        }


        public async Task<UsersProfileViewModel> GetUserInfo(string userId, string activeUserId)
        {
            var posts = await postService.GetPostsById(userId, activeUserId);
            var followers = followService.GetFollowers(userId);
            var followed = followService.GetFollowed(userId);
            var isFollowed = followService.IsFollowed(activeUserId, userId);
            var userInfo = userRepository
                .All()
                .Where(x => x.Id == userId)
                .Select(x => new UsersProfileViewModel()
                {
                    UserId = x.Id,
                    Username = x.UserName,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    PostsViewModels = posts,
                    Followers = followers,
                    Followed = followed,
                    IsFollowed = isFollowed
                })
                .FirstOrDefault();
            return userInfo;
        }

        public async Task EditUserInfo(UserEditInfoModel userInfo, string userId)
        {

            var user = userRepository.All().Where(x => x.Id == userId).FirstOrDefault();

            if (userInfo.UploadImage != null)
            {
                var sb = new StringBuilder();
                var link = UploadProfileImageCloudinary(userId, userInfo.UploadImage).GetAwaiter().GetResult().Split("upload");
                sb.Append(link[0]);
                sb.Append("upload/c_thumb,h_1000,q_auto:good,w_1000");
                sb.Append(link[1]);
                user.ImageUrl = sb.ToString();
            }
            if (userInfo.Description != null && userInfo.Description != user.UserName)
            {
                user.Description = userInfo.Description;
            }
            if (userInfo.FirstName != user.FirstName)
            {
                user.FirstName = userInfo.FirstName;

            }
            if (userInfo.LastName != user.LastName)
            {
                user.LastName = userInfo.LastName;
            }

            userRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }

        private async Task<string> UploadProfileImageCloudinary(string userId, IFormFile UploadImage)
        {
            CloudinaryDotNet.Account account =
                new CloudinaryDotNet.Account("king-arthur", "693651971897844", "JYfv0XETXA21-BnVlBeOmGCrByE");

            CloudinaryDotNet.Cloudinary cloudinary = new CloudinaryDotNet.Cloudinary(account);

            var fileName = $"{userId}_Profile_Picture";

            var stream = UploadImage.OpenReadStream();

            CloudinaryDotNet.Actions.ImageUploadParams uploadParams = new CloudinaryDotNet.Actions.ImageUploadParams()
            {
                File = new FileDescription(UploadImage.FileName, stream),
                PublicId = fileName,
            };

            CloudinaryDotNet.Actions.ImageUploadResult uploadResult = cloudinary.Upload(uploadParams);

            var updatedUrl = cloudinary.GetResource(uploadResult.PublicId).Url;
            return updatedUrl;
        }
    }
}

