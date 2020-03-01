using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
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

            var fileName = $"{userId}_{UploadImage.Name}_Post_Picture";

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

        public ICollection<PostViewModel> GetPostsById(string userId)
        {
            ICollection<PostViewModel> posts;
            if (userId != null)
            {
                posts = postRepository
                   .All()
                   .Where(x => x.AuthorId == userId)
                   .Select(x => new PostViewModel()
                   {
                       UserId = x.AuthorId,
                       UserImageUrl = x.Author.ImageUrl,
                       UserFullName = x.Author.FirstName + " " + x.Author.LastName,
                       PostImageUrl = x.ImageUrl,
                       Likes = x.Likes.Count.ToString(),
                       PostedOn =x.PostedOn,
                   })
                   .OrderByDescending(x=>x.PostedOn)
                   .ToList();
            }
            else
            {
                posts = postRepository
                   .All()
                   .Select(x => new PostViewModel()
                   {
                       UserId = x.AuthorId,
                       UserImageUrl = x.Author.ImageUrl,
                       UserFullName = x.Author.FirstName + " " + x.Author.LastName,
                       PostImageUrl = x.ImageUrl,
                       Likes = x.Likes.Count.ToString(),
                       PostedOn = x.PostedOn,
                   })
                   .OrderByDescending(x => x.PostedOn)
                   .ToList();
            }
            return posts;

        }
    }
}
