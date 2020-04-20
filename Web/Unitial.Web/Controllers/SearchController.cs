using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Unitial.Data.Common.Repositories;
using Unitial.Data.Models;
using Unitial.Web.ViewModels.Search;

namespace Unitial.Web.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        private readonly IRepository<ApplicationUser> userRepository;

        public SearchController(IRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Index(SearchViewModel searchView)
        {
            if (searchView.Text == null || searchView.Text.Replace(" ", "").Replace(" ", "").Length <= 0 || searchView.Text.Length > 80)
            {
                searchView.Users = userRepository.All();
            }
            else
            {
                searchView.Users = userRepository.All().Where(x =>
                (x.FirstName.ToLower().StartsWith(searchView.Text.ToLower()) ||
                x.LastName.ToLower().StartsWith(searchView.Text.ToLower()) ||
                x.FirstName.ToLower().Contains(searchView.Text.ToLower()) ||
                x.LastName.ToLower().Contains(searchView.Text.ToLower()) ||
                (x.FirstName + " " + x.LastName).ToLower().Contains(searchView.Text.ToLower())) &&
                x.IsDeleted == false
                );

            }
            return View(searchView);
        }
    }
}