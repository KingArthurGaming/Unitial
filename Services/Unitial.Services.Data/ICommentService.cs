using System.Collections.Generic;
using Unitial.Web.ViewModels;

namespace Unitial.Services.Data
{
    public interface ICommentService
    {
        public void CreateComment(string postId, string authorId, string text);
        public ICollection<CommetViewModel> GetComments(string postId);
    }
}
