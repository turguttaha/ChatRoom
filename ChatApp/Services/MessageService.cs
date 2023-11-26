using ChatApp.Models;
using Newtonsoft.Json;
using System.Reflection;

namespace ChatApp.Services
{
    public class MessageService
    {
        private readonly LogService _logService;
        public MessageService(LogService logService)
        {
            _logService = logService;
        }
        public string GetPrivateMessagesHtml(int senderId, int receiverId)
        {
            string output = string.Empty;
            string fileName = "/logPrivate_" + senderId + receiverId + ".txt";

            if (!File.Exists(Directory.GetCurrentDirectory() + "/LogFiles/" + fileName))
            {
                fileName = "/logPrivate_" + receiverId + senderId + ".txt";

            }
            try
            {
                List<string> list = _logService.LogRead(fileName);
                foreach (string line in list)
                {
                    Message message = JsonConvert.DeserializeObject<Message>(line);
                    output += "<p> <b>" + message.Sender.NickName + "</b>: " + message.Content + "<br> <sup> " + message.Timestamp + " </sup></p>";
                }
            }
            catch(FileNotFoundException ex)
            {
                output = string.Empty;
            }        

                return output;

    
        }

        public string GetPublicMessagesHtml()
        {
          
            string output = string.Empty;

            List<string> list = _logService.LogRead("logPublic.txt");
            
            foreach (string line in list)
            {
                Message message = JsonConvert.DeserializeObject<Message>(line);
                output += "<p> <b>" + message.Sender.NickName + "</b>: " + message.Content + "<br> <sup> " + message.Timestamp + " </sup></p>";
            }
            return output;
        }

        public void SaveMessage(ChatUser sender, string message) {
         _logService.LogWrite(sender, message);
        }
        public void SaveMessage(ChatUser sender, ChatUser receiver, string message)
        {
            _logService.LogWrite(sender,receiver,message);
        }
    }
}
