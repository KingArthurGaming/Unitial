using System;
using System.Linq;
using Unitial.Data.Models;

namespace Unitial.Web.ViewModels.Message
{
    public class AllConversationsViewModel
    {
        public IQueryable<ConversationViewModel> Conversations { get; set; }
    }
}
