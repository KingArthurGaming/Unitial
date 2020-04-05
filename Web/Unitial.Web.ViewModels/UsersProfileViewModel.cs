using System.Collections.Generic;

namespace Unitial.Web.ViewModels
{
    public class UsersProfileViewModel
    {
        public UsersProfileViewModel()
        {
            PostsViewModels = new List<PostViewModel>();
        }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int Followers { get; set; }
        public int Followed { get; set; }
        public string IsFollowed { get; set; }
        public ICollection<PostViewModel> PostsViewModels { get; set; }
    }
}
