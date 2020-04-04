namespace Unitial.Data.Models
{
    public class Message
    {
        public string Id { get; set; }
        public string Text { get; set; }

        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }

        public string ConversationId { get; set; }
        public Conversation Conversation { get; set; }
    }
}
