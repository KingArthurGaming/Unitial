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
    public class FollowServiceTests
    {
        [Fact]
        public async Task TestFollowServiceFollow()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repo = new EfRepository<Follow>(
                new ApplicationDbContext(options.Options));

            var followService = new FollowService(repo);

            await followService.Follow(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());


            var result = repo.All();
            Assert.Equal(1, result.Count());
        }

        [Fact]
        public async Task TestFollowServiceUnfollow()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repo = new EfRepository<Follow>(
                new ApplicationDbContext(options.Options));

            var followService = new FollowService(repo);

            await followService.Follow(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            await followService.Follow("123456", "987564");
            await followService.Unfollow("123456", "987564");


            var result = repo.All();
            Assert.Equal(1, result.Count());
        }

        [Fact]
        public async Task TestFollowServiceIsFollowedTrue()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repo = new EfRepository<Follow>(
                new ApplicationDbContext(options.Options));

            var followService = new FollowService(repo);

            await followService.Follow(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            await followService.Follow(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            await followService.Follow(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            await followService.Follow("123456", "987564");


            var result = followService.IsFollowed("123456", "987564");
            Assert.Equal("Followed", result);
        }
        [Fact]
        public async Task TestFollowServiceIsFollowedFalse()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repo = new EfRepository<Follow>(
                new ApplicationDbContext(options.Options));

            var followService = new FollowService(repo);

            await followService.Follow(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            await followService.Follow(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
            await followService.Follow(Guid.NewGuid().ToString(), Guid.NewGuid().ToString());


            var result = followService.IsFollowed("123456", "987564");
            Assert.Equal("Not Followed", result);
        }

        [Fact]
        public async Task TestFollowServiceIsFollowCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repo = new EfRepository<Follow>(
                new ApplicationDbContext(options.Options));

            var followService = new FollowService(repo);

            await followService.Follow("123456", Guid.NewGuid().ToString());
            await followService.Follow("123456", Guid.NewGuid().ToString());
            await followService.Follow("123456", Guid.NewGuid().ToString());


            var result = followService.GetFollowed("123456");
            Assert.Equal(3, result);
        }
        [Fact]
        public async Task TestFollowServiceIsFollowersCount()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repo = new EfRepository<Follow>(
                new ApplicationDbContext(options.Options));

            var followService = new FollowService(repo);

            await followService.Follow(Guid.NewGuid().ToString(), "123456");
            await followService.Follow(Guid.NewGuid().ToString(), "123456");
            await followService.Follow(Guid.NewGuid().ToString(), "123456");


            var result = followService.GetFollowers("123456");
            Assert.Equal(3, result);
        }

        [Fact]
        public async Task TestFollowServiceIsGetFollowedIds()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repo = new EfRepository<Follow>(
                new ApplicationDbContext(options.Options));

            var followService = new FollowService(repo);

            await followService.Follow("123456", "1");
            await followService.Follow("123456", "2");
            await followService.Follow("123456", "3");


            var result = followService.GetFollowedIds("123456").ToList();
            Assert.Equal(3, result.Count());
            Assert.Equal("1", result[0]);
            Assert.Equal("2", result[1]);
            Assert.Equal("3", result[2]);
        }

    }
}
