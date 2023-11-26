using ChatApp.Factories;
using ChatApp.Models;
using ChatApp.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;

namespace ChatApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ChatUserService _chatUserService;
        private readonly MessageService _messageService;
        public HomeController( ChatUserService chatUserService, MessageService messageService)
        {
            _chatUserService = chatUserService;
            _messageService = messageService;            
        }

        public IActionResult Index()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser([Bind("NickName")] ChatUser chatUser)
        {

            if (ModelState.IsValid)
            {
                    var user = _chatUserService.CreateNewUser(chatUser);
                    return RedirectToAction("PublicChatPage", new { id = user.Id });
            }

           return RedirectToAction("Index");
        }

        public IActionResult PublicChatPage(int id)
        {
           
            var receivers = _chatUserService.GetAllUsers();
            var sender =  receivers.Find(x => x.Id == id);
            receivers.Remove(sender);
            ChatRoom room = ChatRoomFactory.BuildChatRoom("public", receivers, sender);
            return View(room);
        }
        public IActionResult PrivateChatPage(int id, int senderId)
        {
            List<ChatUser> receivers = new List<ChatUser>();
            var sender = _chatUserService.GetAllUsers().Find(x => x.Id == senderId);
            var receiver = _chatUserService.GetAllUsers().Find(x => x.Id == id);
            receivers.Add(receiver);
            ChatRoom room = ChatRoomFactory.BuildChatRoom("private", receivers, sender);
            return View(room);
        }

        [HttpPost]
        public async Task<JsonResult> MessagePublic(string message, int id)
        {
            
            var user = _chatUserService.GetAllUsers().Find(x => x.Id == id);
            if (user == null)
                throw new ArgumentNullException();
            _messageService.SaveMessage(user, message);
            return Json(new { status = "ok" });
        }
        [HttpPost]
        public async Task<JsonResult> MessagePrivate(int senderId, int receiverId, string message )
        {
            var sender = _chatUserService.GetAllUsers().Find(x => x.Id == senderId);
            var receiver = _chatUserService.GetAllUsers().Find(x => x.Id == receiverId);
            _messageService.SaveMessage(sender,receiver, message);
         
            return Json(new { status = "ok" });
        }
        [HttpGet]
        public async Task<JsonResult> GetPublicMessages()
        {
            try
            {
                string output = _messageService.GetPublicMessagesHtml();

                return Json(new { status = output });
            }
            catch (FileNotFoundException ex)
            {
                return Json(new { status = "error occurred: " + ex.Message});
            }
           
        }
        [HttpPost]
        public async Task<JsonResult> GetPrivateMessages(int senderId, int receiverId)
        {        

            try
            {
                string output = _messageService.GetPrivateMessagesHtml(senderId, receiverId);

                return Json(new { status = output });
            }
            catch (FileNotFoundException ex)
            {
                return Json(new { status = "error occurred: " + ex.Message });
            }
        }
        [HttpPost]
        public async Task<JsonResult> GetChatUsers(int senderId)
        {
            try
            {
                string output = _chatUserService.GetAllUsersHtml(senderId);
                return Json(new { status = output });

            }
            catch (FileNotFoundException ex)
            {
                return Json(new { status = "error occurred: " + ex.Message });
            }

        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}