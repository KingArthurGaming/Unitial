using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unitial.Data.Common.Repositories;
using Unitial.Data.Models;

namespace Unitial.Services.Data
{
    public class LikeService : ILikeService
    {
        private readonly IRepository<Like> likeRepository;

        public LikeService(IRepository<Like> likeRepository)
        {
            this.likeRepository = likeRepository;
        }
        public void LikePost(string postId, string userId)
        {
            var like = new Like()
            {
                UserId = userId,
                PostId = postId,
                LikedOn = DateTime.UtcNow,
            };
            likeRepository.AddAsync(like);
            likeRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }
        public void DislikePost(string postId, string userId)
        {

            var like = likeRepository
                .All()
                .Where(x => x.PostId == postId && x.UserId == userId)
                .FirstOrDefault();
            likeRepository.Delete(like);
            likeRepository.SaveChangesAsync().GetAwaiter().GetResult();
        }

        public string IsLiked(string postId, string userId)
        {
            var like = likeRepository
                .All()
                .Where(x => x.PostId == postId && x.UserId == userId)
                .FirstOrDefault();
            return like != null ? "Yes" : "No";
        }
    }
}
