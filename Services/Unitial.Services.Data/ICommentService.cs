using System.Collections.Generic;
using System.Threading.Tasks;
using Unitial.Web.ViewModels;

namespace Unitial.Services.Data
{
    public interface ICommentService
    {
        public Task CreateComment(string postId, string authorId, string text);
        public ICollection<CommetViewModel> GetComments(string postId);
    }
}
