using System;
using System.Collections.Generic;
using System.Text;

namespace Unitial.Services.Data
{
   public interface ICommentService
    {
        public void CreateComment(string postId , string authorId, string text);
    }
}
