using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Unitial.Data.Models
{
    public class Comment

    {
        public Comment()
        {
            this.CommentOn = DateTime.UtcNow;

        }
        public string Id { get; set; }

        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        public DateTime CommentOn { get; set; }

        public string CommentText { get; set; }

        [ForeignKey("Post")]
        public string PostId { get; set; }

        public Post Post { get; set; }
    }
}
