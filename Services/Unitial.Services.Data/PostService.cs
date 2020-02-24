using System;
using System.Collections.Generic;
using System.Text;
using Unitial.Data.Common.Repositories;
using Unitial.Data.Models;
using Unitial.Web.ViewModels;

namespace Unitial.Services.Data
{
    public class PostService : IPostService
    {
        private readonly IRepository<Post> postRepository;

        public PostService(IRepository<Post> postRepository)
        {
            this.postRepository = postRepository;
        }

        public void CreatePost(CreatePostInputModel createPostInput)
        {
            //TODO: Repository Create Post
        }
    }
}
