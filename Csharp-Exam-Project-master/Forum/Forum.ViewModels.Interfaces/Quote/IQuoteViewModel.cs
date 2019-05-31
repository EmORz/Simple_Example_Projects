using Forum.Models;

namespace Forum.ViewModels.Interfaces.Quote
{
    public interface IQuoteViewModel
    {
        string Id { get; }

        ForumUser Author { get; }

        Models.Reply Reply { get; }

        string Description { get; }
    }
}