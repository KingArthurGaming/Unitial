using System;
using System.Collections.Generic;
using System.Text;
using Unitial.Web.ViewModels;

namespace Unitial.Services.Data
{
    public interface IPostService
    {
        public void CreatePost(CreatePostInputModel createPostInput);
    }
}
