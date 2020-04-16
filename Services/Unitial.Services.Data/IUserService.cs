using System.Threading.Tasks;

namespace Unitial.Services.Data
{
    public interface IUserService
    {
        public Task<string> GetUserIdByUsername(string Username);

    }
}
