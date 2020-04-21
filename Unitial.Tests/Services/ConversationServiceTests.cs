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
    public class ConversationServiceTests
    {
        [Fact]
        public async Task TestConversationServiceCreateConversation()
        {
            var userOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var db = new ApplicationDbContext(userOptions.Options);

            var userRepository = new EfRepository<ApplicationUser>(db);
            var conversationRepository = new EfRepository<Conversation>(db);

            var conversationService = new ConversationService(conversationRepository, userRepository);

            var firstUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var firstUserId = firstUser.Id;
            var secondUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var secondUserId = secondUser.Id;
            await userRepository.AddAsync(firstUser);
            await userRepository.AddAsync(secondUser);
            await userRepository.SaveChangesAsync();

            conversationService.CreateConversation(firstUserId, secondUserId);

            var result = conversationRepository.All();
            Assert.Equal(1, result.Count());
        }
        [Fact]
        public async Task TestConversationServiceCreateConversationManyOfTheSame()
        {
            var userOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var db = new ApplicationDbContext(userOptions.Options);

            var userRepository = new EfRepository<ApplicationUser>(db);
            var conversationRepository = new EfRepository<Conversation>(db);

            var conversationService = new ConversationService(conversationRepository, userRepository);

            var firstUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var firstUserId = firstUser.Id;
            var secondUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var secondUserId = secondUser.Id;
            await userRepository.AddAsync(firstUser);
            await userRepository.AddAsync(secondUser);
            await userRepository.SaveChangesAsync();

            conversationService.CreateConversation(firstUserId, secondUserId);

            var result = conversationRepository.All();
            Assert.Equal(1, result.Count());
        }

        [Fact]
        public async Task TestConversationServiceGetConversationId()
        {
            var userOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var db = new ApplicationDbContext(userOptions.Options);

            var userRepository = new EfRepository<ApplicationUser>(db);
            var conversationRepository = new EfRepository<Conversation>(db);

            var conversationService = new ConversationService(conversationRepository, userRepository);

            var firstUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var firstUserId = firstUser.Id;
            var secondUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var secondUserId = secondUser.Id;
            await userRepository.AddAsync(firstUser);
            await userRepository.AddAsync(secondUser);
            await userRepository.SaveChangesAsync();

            conversationService.CreateConversation(firstUserId, secondUserId);

            var result = conversationService.GetConversationId(firstUserId, secondUserId);
            var reverse = conversationService.GetConversationId(firstUserId, secondUserId);

            Assert.Equal(result, reverse);
            Assert.True(result.Length > 0);
        }

        [Fact]
        public async Task TestConversationServiceDeleteConversation()
        {
            var userOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var db = new ApplicationDbContext(userOptions.Options);

            var userRepository = new EfRepository<ApplicationUser>(db);
            var conversationRepository = new EfRepository<Conversation>(db);

            var conversationService = new ConversationService(conversationRepository, userRepository);

            var firstUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var firstUserId = firstUser.Id;
            var secondUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var secondUserId = secondUser.Id;
            await userRepository.AddAsync(firstUser);
            await userRepository.AddAsync(secondUser);
            await userRepository.SaveChangesAsync();

            conversationService.CreateConversation(firstUserId, secondUserId);
            conversationService.DeleteConversation(firstUserId, secondUserId);

            var result = conversationRepository.All();
            Assert.Equal(0, result.Count());
        }
        [Fact]
        public async Task TestConversationServiceDeleteConversationReverse()
        {
            var userOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var db = new ApplicationDbContext(userOptions.Options);

            var userRepository = new EfRepository<ApplicationUser>(db);
            var conversationRepository = new EfRepository<Conversation>(db);

            var conversationService = new ConversationService(conversationRepository, userRepository);

            var firstUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var firstUserId = firstUser.Id;
            var secondUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var secondUserId = secondUser.Id;
            await userRepository.AddAsync(firstUser);
            await userRepository.AddAsync(secondUser);
            await userRepository.SaveChangesAsync();

            conversationService.CreateConversation(firstUserId, secondUserId);
            conversationService.DeleteConversation(secondUserId, firstUserId);

            var result = conversationRepository.All();
            Assert.Equal(0, result.Count());
        }
        [Fact]
        public async Task TestConversationServiceGetAllConversations()
        {
            var userOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var db = new ApplicationDbContext(userOptions.Options);

            var userRepository = new EfRepository<ApplicationUser>(db);
            var conversationRepository = new EfRepository<Conversation>(db);

            var conversationService = new ConversationService(conversationRepository, userRepository);

            var firstUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var firstUserId = firstUser.Id;
            var secondUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var secondUserId = secondUser.Id;
            var thirdUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var thirdUserId = secondUser.Id;
            await userRepository.AddAsync(firstUser);
            await userRepository.AddAsync(secondUser);
            await userRepository.AddAsync(thirdUser);
            await userRepository.SaveChangesAsync();

            conversationService.CreateConversation(firstUserId, secondUserId);
            conversationService.CreateConversation(firstUserId, thirdUserId);

            var result = conversationService.GetAllConversations(firstUserId);
            Assert.Equal(2, result.Conversations.Count());
        }

        [Fact]
        public async Task TestConversationServiceGetAllConversationsReverse()
        {
            var userOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var db = new ApplicationDbContext(userOptions.Options);

            var userRepository = new EfRepository<ApplicationUser>(db);
            var conversationRepository = new EfRepository<Conversation>(db);

            var conversationService = new ConversationService(conversationRepository, userRepository);

            var firstUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var firstUserId = firstUser.Id;
            var secondUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var secondUserId = secondUser.Id;
            var thirdUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var thirdUserId = secondUser.Id;
            await userRepository.AddAsync(firstUser);
            await userRepository.AddAsync(secondUser);
            await userRepository.AddAsync(thirdUser);
            await userRepository.SaveChangesAsync();

            conversationService.CreateConversation(firstUserId, secondUserId);
            conversationService.CreateConversation(thirdUserId, firstUserId);

            var result = conversationService.GetAllConversations(firstUserId);
            Assert.Equal(2, result.Conversations.Count());
        }




        [Fact]
        public async Task TestConversationServiceIsCreatedTrue()
        {
            var userOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var db = new ApplicationDbContext(userOptions.Options);

            var userRepository = new EfRepository<ApplicationUser>(db);
            var conversationRepository = new EfRepository<Conversation>(db);

            var conversationService = new ConversationService(conversationRepository, userRepository);

            var firstUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var firstUserId = firstUser.Id;
            var secondUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var secondUserId = secondUser.Id;
            var thirdUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var thirdUserId = secondUser.Id;
            await userRepository.AddAsync(firstUser);
            await userRepository.AddAsync(secondUser);
            await userRepository.AddAsync(thirdUser);
            await userRepository.SaveChangesAsync();

            conversationService.CreateConversation(firstUserId, secondUserId);

            Assert.True(conversationService.IsCreated(firstUserId, secondUserId));
        }
        [Fact]
        public async Task TestConversationServiceIsCreatedFalse()
        {
            var userOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var db = new ApplicationDbContext(userOptions.Options);

            var userRepository = new EfRepository<ApplicationUser>(db);
            var conversationRepository = new EfRepository<Conversation>(db);

            var conversationService = new ConversationService(conversationRepository, userRepository);

            var firstUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var firstUserId = firstUser.Id;
            var secondUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var secondUserId = secondUser.Id;
            var thirdUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var thirdUserId = secondUser.Id;
            await userRepository.AddAsync(firstUser);
            await userRepository.AddAsync(secondUser);
            await userRepository.AddAsync(thirdUser);
            await userRepository.SaveChangesAsync();


            Assert.False(conversationService.IsCreated(firstUserId, secondUserId));
        }


        [Fact]
        public async Task TestConversationServiceSetLastUpdate()
        {
            var userOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var db = new ApplicationDbContext(userOptions.Options);

            var userRepository = new EfRepository<ApplicationUser>(db);
            var conversationRepository = new EfRepository<Conversation>(db);

            var conversationService = new ConversationService(conversationRepository, userRepository);

            var firstUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var firstUserId = firstUser.Id;
            var secondUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var secondUserId = secondUser.Id;
            
            await userRepository.AddAsync(firstUser);
            await userRepository.AddAsync(secondUser);
            await userRepository.SaveChangesAsync();

            conversationService.CreateConversation(firstUserId, secondUserId);
            var oldDate = conversationRepository.All().FirstOrDefault().LastUpdate;
            var id = conversationRepository.All().FirstOrDefault().Id;

            conversationService.SetLastUpdate(id);
            var updateDate = conversationRepository.All().FirstOrDefault().LastUpdate;
            
            Assert.True(oldDate< updateDate);
        }
        [Fact]
        public async Task TestConversationServiceSeen()
        {
            var userOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var db = new ApplicationDbContext(userOptions.Options);

            var userRepository = new EfRepository<ApplicationUser>(db);
            var conversationRepository = new EfRepository<Conversation>(db);

            var conversationService = new ConversationService(conversationRepository, userRepository);

            var firstUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var firstUserId = firstUser.Id;
            var secondUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var secondUserId = secondUser.Id;

            await userRepository.AddAsync(firstUser);
            await userRepository.AddAsync(secondUser);
            await userRepository.SaveChangesAsync();

            conversationService.CreateConversation(firstUserId, secondUserId);
            var id = conversationRepository.All().FirstOrDefault().Id;

            conversationRepository.All().FirstOrDefault().SeenFirstUser = false;
            conversationRepository.All().FirstOrDefault().SeenSecondUser = false;
            await conversationRepository.SaveChangesAsync();

            conversationService.Seen(id, firstUserId);
            var converstion = conversationRepository.All().FirstOrDefault();

            Assert.True(converstion.SeenFirstUser);
            Assert.False(converstion.SeenSecondUser);

        }
        [Fact]
        public async Task TestConversationServiceCheckForNew()
        {
            var userOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString());
            var db = new ApplicationDbContext(userOptions.Options);

            var userRepository = new EfRepository<ApplicationUser>(db);
            var conversationRepository = new EfRepository<Conversation>(db);

            var conversationService = new ConversationService(conversationRepository, userRepository);

            var firstUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var firstUserId = firstUser.Id;
            var secondUser = new ApplicationUser()
            {
                FirstName = "Ivan",
                LastName = "Dimitrov",
                UserName = "ivan@abv.bg",
                Birthday = DateTime.UtcNow,
                ImageUrl = "image.com",
            };
            var secondUserId = secondUser.Id;

            await userRepository.AddAsync(firstUser);
            await userRepository.AddAsync(secondUser);
            await userRepository.SaveChangesAsync();

            conversationService.CreateConversation(firstUserId, secondUserId);
            var id = conversationRepository.All().FirstOrDefault().Id;

            conversationRepository.All().FirstOrDefault().SeenFirstUser = false;
            conversationRepository.All().FirstOrDefault().SeenSecondUser = false;
            await conversationRepository.SaveChangesAsync();

            

            Assert.True(conversationService.CheckForNew(firstUserId));

        }
    }
}




