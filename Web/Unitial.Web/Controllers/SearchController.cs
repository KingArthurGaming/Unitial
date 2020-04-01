using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Korzh.EasyQuery.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            if (searchView.Text != null)
            {

                searchView.Users = userRepository.All().Where(x =>
                (x.FirstName.StartsWith(searchView.Text) ||
                x.LastName.StartsWith(searchView.Text) ||
                x.FirstName.Contains(searchView.Text) ||
                x.LastName.Contains(searchView.Text)) &&
                x.IsDeleted == false

                );

            }
            else
            {
                searchView.Users = userRepository.All();

            }
            return View(searchView);
        }
    }
}