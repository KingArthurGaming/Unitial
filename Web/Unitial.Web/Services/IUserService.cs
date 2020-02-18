using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unitial.Web.ViewModels;

namespace Unitial.Web.Services
{
    public interface IUserService
    {
        UsersProfileViewModel GetUserByUsername(string username);
    }
}
