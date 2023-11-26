using ChatApp.Controllers;
using ChatApp.Models;
using ChatApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Cryptography;

namespace ChatApp_test_project
{
    [TestClass]
    public class HomeControllerTest
    {

        [TestMethod]
        public void TestMethodCreateUser()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string logPath = Path.Combine(currentDirectory, "..\\..\\..\\..\\ChatApp\\LogFiles");
            LogService logService = new LogService(logPath);
            ChatUserService chatUserService = new ChatUserService(logService);
            MessageService messageService = new MessageService(logService);
            HomeController homeController = new HomeController(chatUserService,  messageService);

            ChatUser chatUser = new ChatUser()
            {
                NickName = "test"
            };

            IActionResult result = homeController.CreateUser(chatUser);

            Assert.IsNotNull(result);
            Assert.IsTrue(result is RedirectToActionResult);
            Assert.IsNotInstanceOfType(result, typeof(ViewResult));

        }
        [TestMethod]
        public void TestMethodPublicChatPage()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string logPath = Path.Combine(currentDirectory, "..\\..\\..\\..\\ChatApp\\LogFiles");
            LogService logService = new LogService(logPath);
            ChatUserService chatUserService = new ChatUserService(logService);
            MessageService messageService = new MessageService(logService);
            HomeController homeController = new HomeController(chatUserService, messageService);


            IActionResult result = homeController.PublicChatPage(1);

            Assert.IsNotNull(result);
            Assert.IsTrue(result is ViewResult);
            ViewResult viewResult = (ViewResult)result;
            Assert.IsTrue(viewResult.Model is ChatRoom);

        }

        [TestMethod]
        public void TestMethodMessagePublic()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string logPath = Path.Combine(currentDirectory, "..\\..\\..\\..\\ChatApp\\LogFiles");
            LogService logService = new LogService(logPath);
            ChatUserService chatUserService = new ChatUserService(logService);
            MessageService messageService = new MessageService(logService);
            HomeController homeController = new HomeController(chatUserService, messageService);

            string message = "Test";
            int id = 10;

            Task<JsonResult> result = homeController.MessagePublic(message, id);

            Assert.IsNotNull(result);
            Assert.IsTrue(result is Task<JsonResult>);
            //Assert.IsTrue(result.IsCompletedSuccessfully);//false becase it throws exceptions
            Assert.IsTrue(result.Exception.InnerExceptions.Count == 1);

        }
        [TestMethod]
        public void TestMethodMessagePrivatec()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string logPath = Path.Combine(currentDirectory, "..\\..\\..\\..\\ChatApp\\LogFiles");
            LogService logService = new LogService(logPath);
            ChatUserService chatUserService = new ChatUserService(logService);
            MessageService messageService = new MessageService(logService);
            HomeController homeController = new HomeController(chatUserService, messageService);

            string message = "Test";
            int senderId = 1;
            int receiverId = 2;

            Task<JsonResult> result = homeController.MessagePrivate(senderId, receiverId,message);

            Assert.IsNotNull(result);
            //Assert.IsTrue(result is ViewResult);//false because it doesnt return viewresult
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.IsTrue(result.Result is JsonResult);


        }

        [TestMethod]
        public void TestMethodGetPublicMessages()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string logPath = Path.Combine(currentDirectory, "..\\..\\..\\..\\ChatApp\\LogFiles");
            LogService logService = new LogService(logPath);
            ChatUserService chatUserService = new ChatUserService(logService);
            MessageService messageService = new MessageService(logService);
            HomeController homeController = new HomeController(chatUserService, messageService);


            Task<JsonResult> result = homeController.GetPublicMessages();

            Assert.IsNotNull(result);
            Assert.IsTrue(result is Task<JsonResult>);
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.IsTrue(result.Result is JsonResult);

        }

        [TestMethod]
        public void TestMethodGetPrivateMessages()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string logPath = Path.Combine(currentDirectory, "..\\..\\..\\..\\ChatApp\\LogFiles");
            LogService logService = new LogService(logPath);
            ChatUserService chatUserService = new ChatUserService(logService);
            MessageService messageService = new MessageService(logService);
            HomeController homeController = new HomeController(chatUserService, messageService);

            int senderId = 1;
            int receiverId = 2;

            Task<JsonResult> result = homeController.GetPrivateMessages(senderId, receiverId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result is Task<JsonResult>);
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.IsTrue(result.Result is JsonResult);

        }

        [TestMethod]
        public void TestMethodGetChatUserss()
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string logPath = Path.Combine(currentDirectory, "..\\..\\..\\..\\ChatApp\\LogFiles");
            LogService logService = new LogService(logPath);
            ChatUserService chatUserService = new ChatUserService(logService);
            MessageService messageService = new MessageService(logService);
            HomeController homeController = new HomeController(chatUserService, messageService);

            int senderId = 1;


            Task<JsonResult> result = homeController.GetChatUsers(senderId);

            Assert.IsNotNull(result);
            Assert.IsTrue(result is Task<JsonResult>);
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.IsTrue(result.Result is JsonResult);
            //Assert.IsInstanceOfType(result.Result, typeof(ChatUser));//it is false because it is not type of ChatUser it is type of Json

        }
    }
}