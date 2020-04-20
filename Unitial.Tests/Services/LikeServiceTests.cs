using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Unitial.Data;
using Unitial.Data.Common.Repositories;
using Unitial.Data.Models;
using Unitial.Data.Repositories;
using Unitial.Services.Data;
using Xunit;

namespace Unitial.Tests.Services
{
    public class LikeServiceTests
    {
        [Fact]
        public void TestLikeServiceLikePost()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repo = new EfRepository<Like>(
                new ApplicationDbContext(options.Options));

            var likeService = new LikeService(repo);

            likeService.LikePost(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            likeService.LikePost(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            likeService.LikePost(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            likeService.LikePost(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            likeService.LikePost(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            var result = repo.All();
            Assert.Equal(5, result.Count());
        }
        [Fact]
        public void TestLikeServiceWhereLikePost()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repo = new EfRepository<Like>(
                new ApplicationDbContext(options.Options));

            var likeService = new LikeService(repo);

            likeService.LikePost(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            likeService.LikePost(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            likeService.LikePost(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            likeService.LikePost(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            likeService.LikePost("123456789", "UserId");

            var result = repo.All().Where(x=>x.PostId=="123456789").FirstOrDefault();
            Assert.Equal("UserId", result.UserId);
        }


        [Fact]
        public void TestLikeServiceDislikePost()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repo = new EfRepository<Like>(
                new ApplicationDbContext(options.Options));

            var likeService = new LikeService(repo);

            likeService.LikePost(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            likeService.LikePost(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            likeService.LikePost(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            likeService.LikePost(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            likeService.LikePost("123456789", "UserId");

            likeService.DislikePost("123456789", "UserId");

            var result = repo.All();
            Assert.Equal(4, result.Count());
        }


        [Fact]
        public void TestLikeServiceIsLikedYes()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repo = new EfRepository<Like>(
                new ApplicationDbContext(options.Options));

            var likeService = new LikeService(repo);

            likeService.LikePost(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            likeService.LikePost(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            likeService.LikePost(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            likeService.LikePost(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            likeService.LikePost("123456789", "UserId");

            var result = likeService.IsLiked("123456789", "UserId");

            Assert.Equal("Yes", result);
        }

        [Fact]
        public void TestLikeServiceIsLikedNo()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repo = new EfRepository<Like>(
                new ApplicationDbContext(options.Options));

            var likeService = new LikeService(repo);

            likeService.LikePost(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            likeService.LikePost(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            likeService.LikePost(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            likeService.LikePost(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            likeService.LikePost(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());

            var result = likeService.IsLiked("123456789", "UserId");

            Assert.Equal("No", result);
        }


    }
}
