using System.Security.Claims;
using Unitial.Services.Models.Account;
using Unitial.Data.Models;
namespace Unitial.Services
{
    public interface IUserService
    {
        User GetUserByUsername(string userId);
    }
}
