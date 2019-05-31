namespace Forum.Models
{
    public class QuoteReport : Report
    {
        public Quote Quote { get; set; }

        public string QuoteId { get; set; }
    }
}