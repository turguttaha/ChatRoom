using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChatApp.Models
{
    public class ChatUser
    {

        public int Id { get; set; }

        [MinLength(3)]
        public string NickName { get; set; }= string.Empty;
    }
}
