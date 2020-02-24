using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Unitial.Web.Controllers
{
    public class PostController : Controller
    {
        [HttpPost]
        public IActionResult CreatePost(string caption, string imageUrl)
        {
            return View();
        }
    }
}

