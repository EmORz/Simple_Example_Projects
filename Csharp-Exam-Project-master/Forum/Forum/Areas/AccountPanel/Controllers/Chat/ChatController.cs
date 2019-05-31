using Forum.Services.Interfaces.Account;
using Forum.Services.Interfaces.Message;
using Forum.ViewModels.Message;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Areas.AccountPanel.Controllers.Chat
{
    [Area("AccountPanel")]
    [Authorize]
    public class ChatController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IMessageService messageService;

        public ChatController(IAccountService accountService, IMessageService messageService)
        {
            this.accountService = accountService;
            this.messageService = messageService;
        }

        public PartialViewResult MessagesPanel()
        {
            return this.PartialView("_MessagesPanelPartial");
        }

        public PartialViewResult Chat()
        {
            return this.PartialView("_WeclomeChatViewPartial");
        }

        public PartialViewResult RecentConversations()
        {
            this.ViewData["userNames"] = this.accountService.GetUsernames();

            this.ViewData["recentConversations"] = this.messageService.GetRecentConversations(this.User.Identity.Name);

            this.ViewData["unreadMessages"] = this.messageService.GetUnreadMessages(this.User.Identity.Name);

            return this.PartialView("_RecentConversationsPartial");
        }

        [HttpPost]
        public PartialViewResult ChatWithSomebody([FromBody] SendMessageInputModel model)
        {
            var reciever = this.accountService.GetUserByName(model.RecieverName, this.ModelState);

            if (this.ModelState.IsValid)
            {
                var recieverId = reciever.Id;

                model.Messages = this.messageService.GetConversationMessages(this.User.Identity.Name, model.RecieverName, model.ShowAll);
                model.RecieverId = recieverId;

                var result = this.PartialView("_ChatViewPartial", model);

                return result;
            }
            else
            {
                var result = this.PartialView("_ErrorPartial", this.ModelState);

                return result;
            }
        }
    }
}