using ChatApp.Models;

namespace ChatApp.Factories
{
    public class ChatRoomFactory
    {
        private static int _lastId { get; set; } = 1;
        private static int GenerateId()
        {
            _lastId += 1;
            return _lastId;

        }
        public static ChatRoom BuildChatRoom(string roomName, List<ChatUser> users, ChatUser sender)
        {
            switch (roomName.ToLower())
            {
                case "public":
                    ChatRoom publicChatRoom = new ChatRoom();
                    publicChatRoom.Id = 1;
                    publicChatRoom.Name = roomName;
                    publicChatRoom.Receivers =users;
                    publicChatRoom.Sender =sender;
                    return publicChatRoom;
                case "private":
                    ChatRoom privateChatRoom = new ChatRoom();
                    privateChatRoom.Id = GenerateId();
                    privateChatRoom.Receivers = users;
                    privateChatRoom.Name = roomName;
                    privateChatRoom.Sender = sender;
                    return privateChatRoom;

                default:
                   throw new NotImplementedException();
            }
        }
    }
}
