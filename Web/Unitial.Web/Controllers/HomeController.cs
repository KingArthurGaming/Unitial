namespace Unitial.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using Unitial.Services.Data;
    using Unitial.Web.ViewModels;

    public class HomeController : BaseController
    {


        [Authorize]
        public IActionResult Index(string uesrId)
        {
            var user = new UsersProfileViewModel()
            {
                FirstName = "Unitial - ",
                LastName = "Global Wall Page.",
                Description = "Here You Can See Every Post On The Platform",
                ImageUrl = "https://res.cloudinary.com/king-arthur/image/upload/v1582981400/604abd89-83f5-4f0b-89a9-904a693d9a7d_Profile_Picture.png",

            };
            return View(user);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
