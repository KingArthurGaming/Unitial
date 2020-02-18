using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unitial.Data;
using Unitial.Data.Models;
using Unitial.Web.ViewModels;

namespace Unitial.Web.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext db;

        public UserService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public UsersProfileViewModel GetUserByUsername(string username)
        {
            var user = db.Users.Where(x => x.UserName == username).Select(x => new UsersProfileViewModel
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                Description = x.Description,
                ImageUrl = x.ImageUrl
            })
                .FirstOrDefault();
            return user;
        }
    }
}
