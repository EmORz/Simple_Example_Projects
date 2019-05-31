using Forum.Services.Interfaces.Account;
using Forum.Services.Interfaces.Chat;
using Forum.Services.Interfaces.Message;
using Forum.ViewModels.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace Forum.Web.Controllers.Message
{
    [Authorize]
    public class MessageController : BaseController
    {
        private readonly IMessageService messageService;
        private readonly IChatService chatService;

        public MessageController(IAccountService accountService, IMessageService messageService, IChatService chatService) : base(accountService)
        {
            this.messageService = messageService;
            this.chatService = chatService;
        }

        [HttpPost]
        public void Send([FromBody] SendMessageInputModel model)
        {
            var author = this.accountService.GetUser(this.User);

            this.messageService.SendMessage(model, author.Id);
        }

        public void UpdateChat()
        {
            var context = this.HttpContext;

            WebSocket webSocket = null;
            if (context.WebSockets.IsWebSocketRequest)
            {
                this.chatService.OpenChatConnection(webSocket, context, this.User);
            }
        }
    }
}