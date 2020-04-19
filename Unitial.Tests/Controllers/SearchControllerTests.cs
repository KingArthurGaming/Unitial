using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unitial.Data;
using Unitial.Data.Models;
using Unitial.Data.Repositories;
using Unitial.Services.Data;
using Unitial.Web.Controllers;
using Unitial.Web.ViewModels.Search;
using Xunit;

namespace Unitial.Tests.Controllers
{
    public class SearchControllerTests
    {
        [Theory]
        [InlineData("Ivan")]
        [InlineData("Dimitrov")]
        [InlineData("ivo")]
        [InlineData("spasov")]
        [InlineData("KIRO")]
        [InlineData("KIROV")]
        public async Task TestTestSearchUserCase(string name)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repo = new EfRepository<ApplicationUser>(
                new ApplicationDbContext(options.Options));

            var controller = new SearchController(repo);

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
            await repo.AddAsync(new ApplicationUser()
            {
                FirstName = "Ivo",
                LastName = "Spasov",
                UserName = "ivo@abv.bg",
                Birthday = DateTime.UtcNow,
            });
            await repo.AddAsync(new ApplicationUser()
            {
                FirstName = "Stoqn",
                LastName = "Stoqnkov",
                UserName = "stoqn@abv.bg",
                Birthday = DateTime.UtcNow,
            });
            await repo.SaveChangesAsync();

            var searchModel = new SearchViewModel()
            {
                Text = name
            }; 

            var result = controller.Index(searchModel);
            var viewResult = result as ViewResult;
            var model = (SearchViewModel)viewResult.Model;
            var listUsers =  await model.Users.ToListAsync();
            Assert.Equal(1, listUsers.Count);
        }
        [Theory]
        [InlineData("Iv")]
        [InlineData("S")]
        public async Task TestTestSearchUserMany(string name)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repo = new EfRepository<ApplicationUser>(
                new ApplicationDbContext(options.Options));

            var controller = new SearchController(repo);

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
            await repo.AddAsync(new ApplicationUser()
            {
                FirstName = "Ivo",
                LastName = "Spasov",
                UserName = "ivo@abv.bg",
                Birthday = DateTime.UtcNow,
            });
            await repo.AddAsync(new ApplicationUser()
            {
                FirstName = "Stoqn",
                LastName = "Stoqnkov",
                UserName = "stoqn@abv.bg",
                Birthday = DateTime.UtcNow,
            });
            await repo.SaveChangesAsync();

            var searchModel = new SearchViewModel()
            {
                Text = name
            };

            var result = controller.Index(searchModel);
            var viewResult = result as ViewResult;
            var model = (SearchViewModel)viewResult.Model;
            var listUsers = await model.Users.ToListAsync();
            Assert.Equal(2, listUsers.Count);
        }
        [Theory]
        [InlineData("Ivan Dimitrov")]
        [InlineData("Kiro Kirov")]
        [InlineData("IVO SpaSoV")]
        [InlineData("StOQn STOQNKOV")]
        [InlineData("Stoqn Stoqn")]
        public async Task TestSearchUserFullName(string name)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repo = new EfRepository<ApplicationUser>(
                new ApplicationDbContext(options.Options));

            var controller = new SearchController(repo);

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
            await repo.AddAsync(new ApplicationUser()
            {
                FirstName = "Ivo",
                LastName = "Spasov",
                UserName = "ivo@abv.bg",
                Birthday = DateTime.UtcNow,
            });
            await repo.AddAsync(new ApplicationUser()
            {
                FirstName = "Stoqn",
                LastName = "Stoqnkov",
                UserName = "stoqn@abv.bg",
                Birthday = DateTime.UtcNow,
            });
            await repo.SaveChangesAsync();

            var searchModel = new SearchViewModel()
            {
                Text = name
            };

            var result = controller.Index(searchModel);
            var viewResult = result as ViewResult;
            var model = (SearchViewModel)viewResult.Model;
            var listUsers = await model.Users.ToListAsync();
            Assert.Equal(1, listUsers.Count);
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(" ")]
        [InlineData("       ")]
        [InlineData(null)]
        [InlineData("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa")]
        public async Task TestSearchUserReturnAll(string name)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repo = new EfRepository<ApplicationUser>(
                new ApplicationDbContext(options.Options));

            var controller = new SearchController(repo);

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
            await repo.AddAsync(new ApplicationUser()
            {
                FirstName = "Ivo",
                LastName = "Spasov",
                UserName = "ivo@abv.bg",
                Birthday = DateTime.UtcNow,
            });
            await repo.AddAsync(new ApplicationUser()
            {
                FirstName = "Stoqn",
                LastName = "Stoqnkov",
                UserName = "stoqn@abv.bg",
                Birthday = DateTime.UtcNow,
            });
            await repo.SaveChangesAsync();

            var searchModel = new SearchViewModel()
            {
                Text = name
            };

            var result = controller.Index(searchModel);
            var viewResult = result as ViewResult;
            var model = (SearchViewModel)viewResult.Model;
            var listUsers = await model.Users.ToListAsync();
            Assert.Equal(4, listUsers.Count);
        }

        [Fact]
        public async Task TestSearchControllerReturnViewResult()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var repo = new EfRepository<ApplicationUser>(
                new ApplicationDbContext(options.Options));

            var controller = new SearchController(repo);

            await repo.SaveChangesAsync();

            var searchModel = new SearchViewModel()
            {
                Text = "Ivo"
            };

            var result = controller.Index(searchModel);
            Assert.IsType(typeof(ViewResult), result);
        }

    }
}