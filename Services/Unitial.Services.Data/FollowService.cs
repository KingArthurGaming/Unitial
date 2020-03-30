using System.Collections.Generic;
using System.Linq;
using Unitial.Data.Common.Repositories;
using Unitial.Data.Models;

namespace Unitial.Services.Data
{
    public class FollowService : IFollowService
    {
        private readonly IRepository<ApplicationUser> userRepository;
        private readonly IRepository<Follow> followRepository;

        public FollowService(IRepository<ApplicationUser> userRepository, IRepository<Follow> FollowRepository)
        {
            this.userRepository = userRepository;
            followRepository = FollowRepository;
        }
        public string GetMyUserIdByUsername(string username)
        {
            var userId = userRepository
                .All()
                .Where(x => x.UserName == username)
                .Select(x => x.Id)
                .FirstOrDefault();
            return userId;
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
        public void Follow(string follower, string followed)
        {
            var follow = new Follow()
            {
                FollowerId = follower,
                FollowedId = followed,
            };
            followRepository.AddAsync(follow);
            followRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public void Unfollow(string follower, string followed)
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
            var followingCount = followRepository.All().Where(x => x.FollowerId == userId).Select(x=>x.FollowedId).ToList();
            return followingCount;
        }
    }
}
