using System;
using System.Collections.Generic;
using Unitial.Data.Common.Models;

namespace Unitial.Data.Models
{
    public class Post : IDeletableEntity
    {
        public Post()
        {
            this.Comments = new HashSet<Comment>();
            this.Likes = new HashSet<Like>();
            this.PostedOn = DateTime.UtcNow;
            this.IsDeleted = false;
        }
        public string Id { get; set; }
        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }
        public string Caption { get; set; }
        public bool HaveImage { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PostedOn { get; set; }
        public bool HaveLikes { get; set; }
        public bool HaveComments { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Like> Likes { get; set; }
    }
}

