using System;

namespace Unitial.Data.Models
{
    public class Like
    {
        public Like()
        {
            this.LikedOn = DateTime.UtcNow;
        }
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string PostId { get; set; }

        public Post Post { get; set; }

        public DateTime LikedOn { get; set; }
    }
}
