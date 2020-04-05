using System.Collections.Generic;
using Unitial.Data.Models;

namespace Unitial.Web.ViewModels.Message
{
    public class AllMessagesViewModel
    {

        public ApplicationUser ActriveUser { get; set; }
        public string ActriveUserId { get; set; }
        public ApplicationUser ReceiverUser { get; set; }
        public string ReceiverUserId { get; set; }
        public List<Unitial.Data.Models.Message> Messages { get; set; }
    }

}
