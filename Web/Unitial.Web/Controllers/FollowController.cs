using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Unitial.Web.Controllers
{
    public class FollowController : Controller
    {
        public string FollowUser(string FollowedId)
        {
            return "Follow";
        }
    }
}