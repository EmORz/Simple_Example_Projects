using Forum.ViewModels.Interfaces.Message;

namespace Forum.ViewModels.Message
{
    public class UnreadMessageViewModel : IUnreadMessageViewModel
    {
        public string AuthorName { get; set; }

        public int MessagesCount { get; set; }
        
    }
}