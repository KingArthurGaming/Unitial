using System;
using System.Collections.Generic;
using Unitial.Data.Models;

namespace Unitial.Web.ViewModels
{
    public class PostViewModel
    {

        public string UserName { get; set; }
        public string AuthorId { get; set; }
        public string UserImageUrl { get; set; }
        public string ActiveUserImageUrl { get; set; }
        public string ActiveUserId { get; set; }
        public string UserFullName { get; set; }
        public string Caption { get; set; }
        public string PostId { get; set; }
        public string PostImageUrl { get; set; }
        public string Likes { get; set; }
        public DateTime PostedOn { get; set; }
        public bool IsLikedByThisUser { get; set; }
        public bool HaveLikes { get; set; }
        public bool HaveComments { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<CommetViewModel> Comments { get; set; }

    }
}

