using ChatApp.Models;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ChatApp.Services
{
    public class ChatUserService
    {
        private readonly LogService _logService;
        public ChatUserService(LogService logService)
        {
            _logService = logService;
        }
        private int _lastId;
        private int GenerateId()
        {
            var lastUser = GetAllUsers().OrderByDescending(t => t.Id).FirstOrDefault();
            if (lastUser == null)
            {
                _lastId = 0;
            }
            else
            {
                _lastId = lastUser.Id;
            }

            _lastId += 1;
            return _lastId;

        }
        public ChatUser CreateNewUser(ChatUser user)
        {
            var users = GetAllUsers();
            if (users == null) 
                throw new ArgumentNullException(nameof(users));
            ChatUser newUser = users.Find(x=>x.NickName ==user.NickName);
            if (newUser == null)
            {
                user.Id = GenerateId();
                _logService.LogWrite(user);
                return user;
            }
            return newUser;
        }
        public List<ChatUser> GetAllUsers()
        {

            try
            {
                List<string> list = _logService.LogRead("logUser.txt");
                List<ChatUser> users = new List<ChatUser>();
                foreach (var item in list)
                {
                    ChatUser chatUser = JsonConvert.DeserializeObject<ChatUser>(item);
                    users.Add(chatUser);
                }
                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public string GetAllUsersHtml(int senderId)
        {
            var users = GetAllUsers();
            var sender = users.Find(x=>x.Id==senderId);
            users.Remove(sender);
            string output= string.Empty;
            foreach (ChatUser user in users)
            {
                output += @"<tr> <td> <a href = ""/Home/PrivateChatPage/"+ user.Id +@"?senderId=" + senderId+ @"""> "+ user.NickName+ @" </a></td></tr> ";
            }
            return output;
        }

    }
}
