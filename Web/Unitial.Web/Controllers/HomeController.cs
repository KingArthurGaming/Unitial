namespace Unitial.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;
    using Unitial.Services.Data;
    using Unitial.Web.ViewModels;

    public class HomeController : BaseController
    {
        private readonly IPostService postService;
        private readonly IUserService userService;

        public HomeController(IPostService postService, IUserService userService)
        { 
            this.postService = postService;
            this.userService = userService;
        }


        [Authorize]
        public IActionResult Index(string uesrId)
        {
            var username = User.Identity.Name;
            var activeUserId = userService.GetUserByUsername(username);

            var posts = postService.GetPostsById(null, activeUserId);
            var user = new UsersProfileViewModel()
            {
                FirstName = "Home Page",
                Description = "See all your friends posts. And create your own.",
                ImageUrl = "https://res.cloudinary.com/king-arthur/image/upload/v1582981400/604abd89-83f5-4f0b-89a9-904a693d9a7d_Profile_Picture.png",
                PostsViewModels = posts,

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
