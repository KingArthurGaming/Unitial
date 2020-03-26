using System;
using System.Collections.Generic;
using System.Text;

namespace Unitial.Web.ViewModels
{
    public class CommetViewModel
    {
        public string Id { get; set; }

        public string AuthorId { get; set; }

        public string CreatorProfilePic { get; set; }

        public DateTime CommentOn { get; set; }

        public string CommentText { get; set; }

        public string PostId { get; set; }
    }
}
