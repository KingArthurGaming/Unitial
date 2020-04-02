using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unitial.Data.Models;

namespace Unitial.Web.ViewModels.Message
{
    public class ConversationViewModel
    {
        public IQueryable<ApplicationUser> Users { get; set; }
    }
}
