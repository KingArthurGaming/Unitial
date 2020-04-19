using System.Collections.Generic;
using System.Threading.Tasks;

namespace Unitial.Services.Data
{
    public interface IFollowService
    {
        public string IsFollowed(string follower, string followed);
        public Task Unfollow(string follower, string followed);
        public Task Follow(string follower, string followed);
        public int GetFollowers(string userId);
        public int GetFollowed(string userId);
        public ICollection<string> GetFollowedIds(string userId);
    }
}
