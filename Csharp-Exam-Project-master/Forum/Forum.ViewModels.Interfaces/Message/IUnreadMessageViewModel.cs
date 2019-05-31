namespace Forum.ViewModels.Interfaces.Message
{
    public interface IUnreadMessageViewModel
    {
        string AuthorName { get; }

        int MessagesCount { get; }
    }
}