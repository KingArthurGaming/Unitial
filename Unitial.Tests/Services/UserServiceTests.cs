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
    public class UserServiceTests
    {
        [Fact]
        public async Task TestUserServiceGetUserIdByUsername()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repo = new EfRepository<ApplicationUser>(
                new ApplicationDbContext(options.Options));

            var userService = new UserService(repo);

            await repo.AddAsync(new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
            });
            await repo.AddAsync(new ApplicationUser()
            {
                FirstName = "Kiro",
                LastName = "Kirov",
                UserName = "kiro@abv.bg",
                Birthday = DateTime.UtcNow,
            });
            await repo.SaveChangesAsync();

            var result = await userService.GetUserIdByUsername("kiro@abv.bg");
            Assert.IsType<string>(result);
            Assert.True(result.Length>0);
        }

        [Fact]
        public async Task TestUserServiceGetUserIdByUsernameNoUser()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repo = new EfRepository<ApplicationUser>(
                new ApplicationDbContext(options.Options));

            var userService = new UserService(repo);

            var result = await userService.GetUserIdByUsername("kiro@abv.bg");
            Assert.Equal("No such user exist.", result );
        }

    }
}