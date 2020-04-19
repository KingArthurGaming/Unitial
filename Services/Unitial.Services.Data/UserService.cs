using System.Linq;
using System.Threading.Tasks;
using Unitial.Data.Common.Repositories;
using Unitial.Data.Models;

namespace Unitial.Services.Data
{
    public class UserService : IUserService
    {
        private readonly IRepository<ApplicationUser> userRepository;

        public UserService(IRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }


        public async Task<string> GetUserIdByUsername(string username)
        {
            var userId = userRepository
                .All()
                .Where(x => x.UserName == username)
                .Select(x => x.Id)
                .FirstOrDefault();
            if (userId==null)
            {
                return "No such user exist.";
            }
            return userId;
        }

    }
}
