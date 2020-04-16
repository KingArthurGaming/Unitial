using System;
using System.Collections.Generic;
using System.Text;
using Unitial.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Unitial.Services.Data
{
    public interface IPostService
    {
        public Task CreatePost(CreatePostInputModel createPostInput, string userId);
        public Task<ICollection<PostViewModel>> GetPostsById(string userId, string activeUserId);
        public Task DeletePost(string postId);

    }
}
