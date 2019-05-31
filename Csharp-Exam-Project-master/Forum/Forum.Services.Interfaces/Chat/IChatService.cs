using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Security.Claims;

namespace Forum.Services.Interfaces.Chat
{
    public interface IChatService
    {
        void OpenChatConnection(WebSocket webSocket, HttpContext httpContext, ClaimsPrincipal principal);
    }
}