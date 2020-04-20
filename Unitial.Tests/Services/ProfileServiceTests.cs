using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unitial.Data;
using Unitial.Data.Models;
using Unitial.Data.Repositories;
using Unitial.Services.Data;
using Unitial.Web.ViewModels;
using Xunit;

namespace Unitial.Tests.Services
{
    public class ProfileServiceTests
    {
        [Fact]
        public async Task TestProfileServiceGetUserInfo()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repo = new EfRepository<ApplicationUser>(
                new ApplicationDbContext(options.Options));

            var profileService = new ProfileService(repo, new MockPostService(), new MockFollowService());
            var user = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
            };
            var userId = user.Id;

            await repo.AddAsync(user);
            await repo.AddAsync(new ApplicationUser()
            {
                FirstName = "Kiro",
                LastName = "Kirov",
                UserName = "kiro@abv.bg",
                Birthday = DateTime.UtcNow,
            });
            await repo.SaveChangesAsync();

            var result =  profileService.GetUserInfo(userId,"123");
            Assert.IsType<UsersProfileViewModel>(result);
            Assert.Equal("Ivan", result.FirstName);
            Assert.Equal("Dimitrov", result.LastName);
            Assert.Equal(userId, result.UserId);
            Assert.Equal(1, result.Followed);
            Assert.Equal(1, result.Followers);
            Assert.Equal("Followed", result.IsFollowed);
            Assert.Equal("Followed", result.IsFollowed);


        }



    }

    public class MockPostService : IPostService
    {
        public Task CreatePost(CreatePostInputModel createPostInput, string userId)
        {
            throw new NotImplementedException();
        }

        public Task DeletePost(string postId)
        {
            throw new NotImplementedException();
        }

        public ICollection<PostViewModel> GetPostsById(string userId, string activeUserId)
        {
            return new List<PostViewModel>();
        }
    }

    public class MockFollowService : IFollowService
    {
        public Task Follow(string follower, string followed)
        {
            throw new NotImplementedException();
        }

        public int GetFollowed(string userId)
        {
            return 1;
        }

        public ICollection<string> GetFollowedIds(string userId)
        {
            throw new NotImplementedException();
        }

        public int GetFollowers(string userId)
        {
            return 1;
        }

        public string IsFollowed(string follower, string followed)
        {
            return "Followed";
        }

        public Task Unfollow(string follower, string followed)
        {
            throw new NotImplementedException();
        }
    }
}