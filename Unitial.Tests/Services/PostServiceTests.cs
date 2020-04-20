using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unitial.Data;
using Unitial.Data.Models;
using Unitial.Data.Repositories;
using Unitial.Services.Data;
using Unitial.Web.ViewModels;
using Unitial.Web.ViewModels.Message;
using Xunit;

namespace Unitial.Tests.Services
{
    public class PostServiceTests
    {
        [Fact]
        public async Task TestPostServiceCreatePost()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var postRepo = new EfRepository<Post>(new ApplicationDbContext(options.Options));
            var userRepo = new EfRepository<ApplicationUser>(new ApplicationDbContext(options.Options));
            var likeRepo = new EfRepository<Like>(new ApplicationDbContext(options.Options));
            var sender = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
            };
            var senderId = sender.Id;
            var receiver = new ApplicationUser()
            {
                FirstName = "Kiro",
                LastName = "Kirov",
                UserName = "kiro@abv.bg",
                Birthday = DateTime.UtcNow,
            };
            var receiverId = receiver.Id;

            await userRepo.AddAsync(sender);
            await userRepo.AddAsync(receiver);
            await userRepo.SaveChangesAsync();

            var postService = new PostService(postRepo, userRepo, likeRepo, new MockCommentService(), new MockFollowService());

            var createPostInput = new CreatePostInputModel() { };

            await postService.CreatePost(createPostInput, senderId);

            var result = await postRepo.All().CountAsync();
            Assert.Equal(1, result);

        }
        [Fact]
        public async Task TestPostServiceGetPosts()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var db = new ApplicationDbContext(options.Options);
            var postRepo = new EfRepository<Post>(db);
            var userRepo = new EfRepository<ApplicationUser>(db);
            var likeRepo = new EfRepository<Like>( db);
            var sender = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "Image",
            };
            var senderId = sender.Id;
            var receiver = new ApplicationUser()
            {
                FirstName = "Kiro",
                LastName = "Kirov",
                UserName = "kiro@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "Image",

            };
            var receiverId = receiver.Id;

            await userRepo.AddAsync(sender);
            await userRepo.AddAsync(receiver);
            await userRepo.SaveChangesAsync();

            var postService = new PostService(postRepo, userRepo, likeRepo, new MockCommentService(), new MockFollowService());

            var createPostInput = new CreatePostInputModel() { };

            await postService.CreatePost(createPostInput, senderId);
            await postService.CreatePost(createPostInput, senderId);

            var result =  postService.GetPostsById(senderId,receiverId).ToList();
            Assert.Equal(2, result.Count);
            Assert.Equal(receiverId, result[0].ActiveUserId);
            Assert.Equal("Image", result[0].UserImageUrl);
            Assert.Equal(sender.FirstName+" "+ sender.LastName, result[0].UserFullName);
        }


        public class MockCommentService : ICommentService
        {
            public Task CreateComment(string postId, string authorId, string text)
            {
                throw new NotImplementedException();
            }

            public ICollection<CommetViewModel> GetComments(string postId)
            {
                var comments = new List<CommetViewModel>();
                return comments;
            }
        }
        public class MockFollowService : IFollowService
        {
            public string IsFollowed(string follower, string followed)
            {
                throw new NotImplementedException();
            }

            public void Unfollow(string follower, string followed)
            {
                throw new NotImplementedException();
            }

            public void Follow(string follower, string followed)
            {
                throw new NotImplementedException();
            }

            public int GetFollowers(string userId)
            {
                throw new NotImplementedException();
            }

            public int GetFollowed(string userId)
            {
                throw new NotImplementedException();
            }

            public ICollection<string> GetFollowedIds(string userId)
            {
                throw new NotImplementedException();
            }

            
        }
    }
}