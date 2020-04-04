using System.Linq;
using Unitial.Data.Models;

namespace Unitial.Web.ViewModels.Search
{
    public class SearchViewModel
    {
        public IQueryable<ApplicationUser> Users { get; set; }
        public string Text { get; set; }
    }
}
