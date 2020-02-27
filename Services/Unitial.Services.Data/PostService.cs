using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using Unitial.Data.Common.Repositories;
using Unitial.Data.Models;
using Unitial.Web.ViewModels;

namespace Unitial.Services.Data
{
    public class PostService : IPostService
    {
        private readonly IRepository<Post> postRepository;
        private readonly IRepository<ApplicationUser> userRepository;


        public PostService(IRepository<Post> postRepository, IRepository<ApplicationUser> userRepository)
        {
            this.postRepository = postRepository;
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


        public void CreatePost(CreatePostInputModel createPostInput, string userId)
        {
            var imageUrl = UploadPostCloudinary(userId, createPostInput.UploadImage);
            var post = new Post()
            {
                Id = Guid.NewGuid().ToString(),
                AuthorId = userId,
                Caption = createPostInput.Caption,
                HaveImage = true,
                ImageUrl = imageUrl//createPostInput.ImageUrl
            };
            postRepository.AddAsync(post).GetAwaiter().GetResult();
            postRepository.SaveChangesAsync().GetAwaiter().GetResult();

        }

        private string UploadPostCloudinary(string userId, IFormFile UploadImage)
        {
            CloudinaryDotNet.Account account =
                new CloudinaryDotNet.Account("king-arthur", "693651971897844", "JYfv0XETXA21-BnVlBeOmGCrByE");

            CloudinaryDotNet.Cloudinary cloudinary = new CloudinaryDotNet.Cloudinary(account);

            var fileName = $"{userId}_{UploadImage.FileName}_Post_Picture";

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
