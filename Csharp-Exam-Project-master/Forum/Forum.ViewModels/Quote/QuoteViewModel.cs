using Forum.MapConfiguration.Contracts;
using Forum.Models;
using Forum.ViewModels.Interfaces.Quote;

namespace Forum.ViewModels.Quote
{
    public class QuoteViewModel : IQuoteViewModel, IMapFrom<Models.Quote>
    {
        public string Id { get; set; }

        public ForumUser Author { get; set; }

        public Models.Reply Reply { get; set; }

        public string Description { get; set; }
    }
}