namespace Forum.ViewModels.Interfaces.Quote
{
    public interface IQuoteInputModel
    {
        string Id { get; set; }

        string ReplyId { get; set; }

        string Description { get; set; }

        string Quote { get; set; }

        string RecieverId { get; set; }

        string QuoteRecieverId { get; set; }
        }
}