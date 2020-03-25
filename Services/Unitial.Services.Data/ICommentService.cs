using System;
using System.Collections.Generic;
using System.Text;
using Unitial.Data.Models;

namespace Unitial.Services.Data
{
    public interface ICommentService
    {
        public void CreateComment(string postId, string authorId, string text);
        public ICollection<Comment> GetComments(string postId);
    }
}
