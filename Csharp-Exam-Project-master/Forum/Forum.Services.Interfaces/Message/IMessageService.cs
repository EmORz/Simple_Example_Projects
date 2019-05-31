using Forum.ViewModels.Interfaces.Message;
using System.Collections.Generic;

namespace Forum.Services.Interfaces.Message
{
    public interface IMessageService
    {
        IEnumerable<Models.Message> GetConversationMessages(string firstPersonName, string secondPersonName, bool showAll);

        int SendMessage(ISendMessageInputModel model, string authorId);

        IEnumerable<IChatMessageViewModel> GetLatestMessages(string lastDate, string loggedInUser, string otherUserId);

        IEnumerable<string> GetRecentConversations(string username);

        int RemoveUserMessages(string username);

        IEnumerable<IUnreadMessageViewModel> GetUnreadMessages(string username);
    }
}