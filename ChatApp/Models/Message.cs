using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models
{
    public class Message
    {
        public int Id { get; set; }
        [MinLength(1)]
        public string Content { get; set; } = string.Empty;
        public ChatUser Sender { get; set; } 
        public DateTime Timestamp { get; set; }
    }
}
