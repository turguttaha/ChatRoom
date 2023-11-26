using System;
using System.IO;
using ChatApp.Models;
using Newtonsoft.Json;

namespace ChatApp.Services
{
    public class LogService
    {
        private readonly string _logFilePath;

        public LogService(string logFilePath)
        {
            _logFilePath = logFilePath;
        }

        public void LogWrite(ChatUser user)
        {
            var json = JsonConvert.SerializeObject(user);
            string fileName = "/logUser.txt";
            string logfilePath = _logFilePath + fileName;
            if (!File.Exists(logfilePath))
                throw new FileNotFoundException(fileName + " isn't found!");
            using (var writer = new StreamWriter(logfilePath, true))
            {
                writer.WriteLine(json);
            }
        }
        public void LogWrite(ChatUser user, string message)
        {



            Message logData = new Message
            {
                Timestamp = DateTime.UtcNow,
                Sender = user,
                Content = message
            };


            var json = JsonConvert.SerializeObject(logData);
            string fileName = "/logPublic.txt";
            string logfilePath = _logFilePath + fileName;
            if (!File.Exists(logfilePath))
                throw new FileNotFoundException(fileName + " isn't found!");
            using (var writer = new StreamWriter(logfilePath, true))
            {
                writer.WriteLine(json);
            }
        }

        public void LogWrite(ChatUser sender, ChatUser receiver, string message)
        {

            string fileName = "/logPrivate_"+sender.Id+receiver.Id+".txt";
            if (!File.Exists(_logFilePath + fileName))
            {
                fileName = "/logPrivate_" + receiver.Id + sender.Id + ".txt";

            }

            Message logData = new Message
            {
                Timestamp = DateTime.UtcNow,
                Sender = sender,
                Content = message
            };

            var json = JsonConvert.SerializeObject(logData);

            using (var writer = new StreamWriter(_logFilePath + fileName, true))
            {
                writer.WriteLine(json);
            }
        }
        public List<string> LogRead(string fileName)
        {

            string filePath = _logFilePath + "/" + fileName; // replace with your file path
            if(!File.Exists(filePath))
                throw new FileNotFoundException(fileName+" isn't found!");
            List<string> lines = new List<string>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                   
                    lines.Add(line);                    
                }
            }
            return lines;
        }
    }

}

