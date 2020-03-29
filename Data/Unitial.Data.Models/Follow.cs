using System;
using System.Collections.Generic;
using System.Text;

namespace Unitial.Data.Models
{
    public class Follow
    {
        public Follow()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string FollowerId { get; set; }
        public ApplicationUser Follower { get; set; }
        public string FollowedId { get; set; }
        public ApplicationUser Followed { get; set; }

    }
}
