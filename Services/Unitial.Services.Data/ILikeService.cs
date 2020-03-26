using System;
using System.Collections.Generic;
using System.Text;

namespace Unitial.Services.Data
{
    public interface ILikeService
    {
        public void LikePost(string postId, string userId);
        public void DislikePost(string postId, string userId);
        public string IsLiked(string postId, string userId);
    }
}
