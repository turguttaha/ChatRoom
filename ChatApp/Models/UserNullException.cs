namespace ChatApp.Models
{
    public class UserNullException : Exception
    {
        public UserNullException(string message):base(message) { }
    }
}
