using System;
using System.Collections.Generic;
using System.Text;
using Unitial.Web.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Unitial.Services.Data
{
    public interface IPostService
    {
        public void CreatePost(CreatePostInputModel createPostInput, string userId);
        public string GetMyUserIdByUsername(string username);
        public ICollection<PostViewModel> GetPostsById(string userId, string activeUserId);
        public void DeletePost(string postId);
        public void LikePost(string postId, string userId);
        public void DislikePost(string postId, string userId);
        public string IsLiked(string postId, string userId);
    }
}
