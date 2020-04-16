using CloudinaryDotNet;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unitial.Data.Common.Repositories;
using Unitial.Data.Models;
using Unitial.Web.ViewModels;

namespace Unitial.Services.Data
{
    public class PostService : IPostService
    {
        private readonly IRepository<Post> postRepository;
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IRepository<Like> likeRepository;
        private readonly ICommentService commentService;
        private readonly IFollowService followService;

        public PostService(IRepository<Post> postRepository, IRepository<ApplicationUser> userRepository, IRepository<Like> likeRepository, ICommentService commentService, IFollowService followService)
        {
            this.postRepository = postRepository;
            this.userRepository = userRepository;
            this.likeRepository = likeRepository;
            this.commentService = commentService;
            this.followService = followService;
        }


        public async Task CreatePost(CreatePostInputModel createPostInput, string userId)
        {
            var likes = false;
            var comments = false;
            var imageUrl = await UploadPostCloudinary(userId, createPostInput.UploadImage);
            if (createPostInput.Likes == "on")
            {
                likes = true;
            }
            if (createPostInput.Comments == "on")
            {
                comments = true;

            }
            var post = new Post()
            {
                Id = Guid.NewGuid().ToString(),
                AuthorId = userId,
                Caption = createPostInput.Caption,
                HaveImage = true,
                ImageUrl = imageUrl,
                HaveLikes = likes,
                HaveComments = comments,
            };
            await postRepository.AddAsync(post);
            await postRepository.SaveChangesAsync();

        }

        private async Task<string> UploadPostCloudinary(string userId, IFormFile UploadImage)
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

        public async Task<ICollection<PostViewModel>> GetPostsById(string userId, string activeUserId)
        {
            ICollection<PostViewModel> posts;
            if (userId != null)
            {
                posts = postRepository
                   .All()
                   .Where(x => x.AuthorId == userId && x.IsDeleted == false)
                   .Select(x => new PostViewModel()
                   {
                       UserName = x.Author.UserName,
                       AuthorId = x.AuthorId,
                       UserImageUrl = x.Author.ImageUrl,
                       UserFullName = x.Author.FirstName + " " + x.Author.LastName,
                       Caption = x.Caption,
                       PostId = x.Id,
                       PostImageUrl = x.ImageUrl,
                       Likes = x.Likes.Count.ToString(),
                       PostedOn = x.PostedOn,
                       ActiveUserImageUrl = userRepository.All().Where(y => y.Id == activeUserId).Select(x => x.ImageUrl).FirstOrDefault(),
                       ActiveUserId = activeUserId,
                       IsLikedByThisUser = likeRepository.All().Where(Y => Y.PostId == x.Id && Y.UserId == activeUserId).Any() ? true : false,
                       Comments =  commentService.GetComments(x.Id).GetAwaiter().GetResult(),
                       HaveLikes = x.HaveLikes,
                       HaveComments = x.HaveComments,
                       IsDeleted = x.IsDeleted

                   })
                   .OrderByDescending(x => x.PostedOn)
                   .ToList();
            }
            else
            {
                var following = followService.GetFollowedIds(activeUserId);
                posts = postRepository
                    .All()
                   .Where(x => (x.IsDeleted == false && following.Contains(x.AuthorId)) || (x.IsDeleted == false && x.AuthorId == activeUserId))
                   .Select(x => new PostViewModel()
                   {
                       UserName = x.Author.UserName,
                       AuthorId = x.AuthorId,
                       UserImageUrl = x.Author.ImageUrl,
                       UserFullName = x.Author.FirstName + " " + x.Author.LastName,
                       Caption = x.Caption,
                       PostId = x.Id,
                       PostImageUrl = x.ImageUrl,
                       Likes = x.Likes.Count.ToString(),
                       PostedOn = x.PostedOn,
                       ActiveUserId = activeUserId,
                       ActiveUserImageUrl = userRepository.All().Where(y => y.Id == activeUserId).Select(x => x.ImageUrl).FirstOrDefault(),
                       IsLikedByThisUser = likeRepository.All().Where(Y => Y.PostId == x.Id && Y.UserId == activeUserId).Any() ? true : false,
                       Comments = commentService.GetComments(x.Id).GetAwaiter().GetResult(),
                       HaveLikes = x.HaveLikes,
                       HaveComments = x.HaveComments,
                       IsDeleted = x.IsDeleted
                   })
                   .OrderByDescending(x => x.PostedOn)
                   .ToList();
            }
            return posts;

        }

        public async Task DeletePost(string postId)
        {
            var post = postRepository.All().Where(x => x.Id == postId).FirstOrDefault();
            post.IsDeleted = true;
            await postRepository.SaveChangesAsync();
        }


    }
}
