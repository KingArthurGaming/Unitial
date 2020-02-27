using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using Unitial.Data.Common.Repositories;
using Unitial.Data.Models;
using Unitial.Web.ViewModels;

namespace Unitial.Services.Data
{
    public class ProfileService : IProfileService
    {
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IRepository<Post> postRepository;

        public ProfileService(
            IRepository<ApplicationUser> userRepository,
            IRepository<Post> postRepository)
        {
            this.userRepository = userRepository;
            this.postRepository = postRepository;
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
            var posts = postRepository
                .All()
                .Where(x => x.AuthorId == userId)
                .Select(x => new PostViewModel()
                {
                    UserId = x.AuthorId,
                    UserImageUrl = x.Author.ImageUrl,
                    UserFullName = x.Author.FirstName + " " + x.Author.LastName,
                    PostImageUrl = x.ImageUrl,
                    Likes = x.Likes.Count.ToString(),
                }).ToList();


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
                    PostsViewModels = posts
                })
                .FirstOrDefault();
            return userInfo;
        }

        public void EditUserInfo(UserEditInfoModel userInfo, string userId)
        {

            var user = userRepository.All().Where(x => x.Id == userId).FirstOrDefault();

            if (userInfo.UploadImage != null )
            {
                user.ImageUrl = UploadProfileImageCloudinary(userId, userInfo.UploadImage);
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

        private string UploadProfileImageCloudinary(string userId, IFormFile UploadImage)
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

