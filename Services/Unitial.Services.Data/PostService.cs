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

            var post = new Post()
            {
                Id = Guid.NewGuid().ToString(),
                AuthorId = userId,
                Caption = createPostInput.Caption,
                HaveImage = true,
                ImageUrl = createPostInput.ImageUrl
            };
            postRepository.AddAsync(post).GetAwaiter().GetResult();
            postRepository.SaveChangesAsync().GetAwaiter().GetResult();

        }
    }
}
