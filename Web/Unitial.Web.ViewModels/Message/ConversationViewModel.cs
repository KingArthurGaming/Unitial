using System;
using System.Linq;
using Unitial.Data.Models;

namespace Unitial.Web.ViewModels.Message
{
    public class ConversationViewModel
    {
        public ApplicationUser User { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
