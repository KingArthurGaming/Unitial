using System;

namespace Unitial.Web.ViewModels
{
    public class PostViewModel
    {
        public string UserId { get; set; }
        public string UserImageUrl { get; set; }
        public string UserFullName { get; set; }
        public string PostImageUrl { get; set; }
        public string Likes { get; set; }
        public DateTime PostedOn { get; set; }
    }
}

