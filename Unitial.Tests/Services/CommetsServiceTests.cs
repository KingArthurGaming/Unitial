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
    public class CommetServiceTests
    {
        [Fact]
        public async Task TestCommetServiceCreateCommet()
        {
            var userOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var userRepository = new EfRepository<ApplicationUser>(
                new ApplicationDbContext(userOptions.Options));

            var commentOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var commentRepository = new EfRepository<Comment>(
                new ApplicationDbContext(commentOptions.Options));

            var commentService = new CommentService(commentRepository, userRepository);

            await commentService.CreateComment("", Guid.NewGuid().ToString(), "mnogo qko");
            await commentService.CreateComment("", Guid.NewGuid().ToString(), "asd");
            await commentService.CreateComment("", Guid.NewGuid().ToString(), "qwe");
            await commentService.CreateComment("", Guid.NewGuid().ToString(), "asd");
            await commentService.CreateComment("", Guid.NewGuid().ToString(), "qwe");

            var result = commentRepository.All();
            Assert.Equal(5, result.Count());
        }

        [Fact]
        public async Task TestCommetServiceGetCommets()
        {
            var commentOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("CommentsTest");
            var database = new ApplicationDbContext(commentOptions.Options);
            var userRepository = new EfRepository<ApplicationUser>(database);

            var commentRepository = new EfRepository<Comment>(database);

            var commentService = new CommentService(commentRepository, userRepository);

            var user = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var userid = user.Id;
            await userRepository.AddAsync(user);
            await userRepository.SaveChangesAsync();

            await commentService.CreateComment("123", userid, "mnogo qko");
            await commentService.CreateComment("123", userid, "asd");

            var result = commentService.GetComments("123");
            Assert.Equal(2, result.Count());
        }


    }
}
