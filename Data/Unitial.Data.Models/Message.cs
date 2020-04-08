using System;

namespace Unitial.Data.Models
{
    public class Message
    {
        public Message()
        {
            this.Id = Guid.NewGuid().ToString();
            this.SendedOn = DateTime.UtcNow;
        }
        public string Id { get; set; }
        public string Text { get; set; }
        public DateTime SendedOn { get; set; }

        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }

        public string ConversationId { get; set; }
        public Conversation Conversation { get; set; }
    }
}
