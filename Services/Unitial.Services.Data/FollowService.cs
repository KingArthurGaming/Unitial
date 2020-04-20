using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unitial.Data.Common.Repositories;
using Unitial.Data.Models;

namespace Unitial.Services.Data
{
    public class FollowService : IFollowService
    {
        private readonly IRepository<Follow> followRepository;

        public FollowService(IRepository<Follow> FollowRepository)
        {
            followRepository = FollowRepository;
        }


        public string IsFollowed(string follower, string followed)
        {
            var isFollowed = followRepository.All().Where(x => x.FollowedId == followed && x.FollowerId == follower);
            if (isFollowed.Any())
            {
                return "Followed";
            }
            return "Not Followed";

        }
        public async Task Follow(string follower, string followed)
        {

            var follow = new Follow()
            {
                FollowerId = follower,
                FollowedId = followed,
            };
            await followRepository.AddAsync(follow);
            await followRepository.SaveChangesAsync();
        }

        public async Task Unfollow(string follower, string followed)
        {
            var follow = followRepository.All().Where(x => x.FollowedId == followed && x.FollowerId == follower).FirstOrDefault();

            followRepository.Delete(follow);
             followRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public int GetFollowers(string userId)
        {
            var followersCount = followRepository.All().Where(x => x.FollowedId == userId).Count();
            return followersCount;
        }

        public int GetFollowed(string userId)
        {
            var followingCount = followRepository.All().Where(x => x.FollowerId == userId).Count();
            return followingCount;
        }

        public ICollection<string> GetFollowedIds(string userId)
        {
            var followingCount = followRepository.All().Where(x => x.FollowerId == userId).Select(x => x.FollowedId).ToList();
            return followingCount;
        }
    }
}
