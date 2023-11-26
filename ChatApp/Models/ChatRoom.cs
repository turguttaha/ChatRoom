namespace ChatApp.Models
{
    public class ChatRoom
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ChatUser Sender { get; set; }
        public List<ChatUser>  Receivers { get; set; }
    }
}
