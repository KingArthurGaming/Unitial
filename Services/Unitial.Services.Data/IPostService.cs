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
        public ICollection<PostViewModel> GetPostsById(string userId, string activeUserId);
        public void DeletePost(string postId);
        
    }
}
