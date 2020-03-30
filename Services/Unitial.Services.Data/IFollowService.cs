using System.Collections.Generic;

namespace Unitial.Services.Data
{
    public interface IFollowService
    {
        public string GetMyUserIdByUsername(string username);
        public string IsFollowed(string follower, string followed);
        public void Unfollow(string follower, string followed);
        public void Follow(string follower, string followed);
        public int GetFollowers(string userId);
        public int GetFollowed(string userId);
        public ICollection<string> GetFollowedIds(string userId);

    }
}
