using Forum.Services.Common;
using Forum.Services.Interfaces.Chat;
using Forum.Services.Interfaces.Message;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text;
using System.Threading;

namespace Forum.Services.Chat
{
    public class ChatService : IChatService
    {
        private readonly IMessageService messageService;

        public ChatService(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        public void OpenChatConnection(WebSocket webSocket, HttpContext context, ClaimsPrincipal principal)
        {
            webSocket = context.WebSockets.AcceptWebSocketAsync().GetAwaiter().GetResult();

            while (true)
            {
                var byteArr = new byte[4096];

                webSocket.ReceiveAsync(byteArr, CancellationToken.None).GetAwaiter().GetResult();

                var result = Encoding.UTF8.GetString(byteArr);

                var splittedResult = result.Split(ServicesConstants.MessagesSeparator);

                string date = splittedResult[0];

                string otherUserId = splittedResult[1];
                
                var messages = this.messageService.GetLatestMessages(date, principal.Identity.Name, otherUserId);

                var jsonStr = JsonConvert.SerializeObject(messages);

                var testArr = Encoding.UTF8.GetBytes(jsonStr);

                webSocket.SendAsync(
               buffer: new ArraySegment<byte>(
                   array: testArr,
                   offset: 0,
                   count: testArr.Length),
               messageType: WebSocketMessageType.Text,
               endOfMessage: true,
               cancellationToken: CancellationToken.None).GetAwaiter().GetResult();
            }
        }
    }
}