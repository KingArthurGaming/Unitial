using System;
using System.Collections.Generic;
using System.Text;

namespace Unitial.Web.ViewModels.Message
{
    public class MessageInputModel
    {
        public string Text { get; set; }

        public string SenderId { get; set; }

        public string ConversationId { get; set; }
    }
}
