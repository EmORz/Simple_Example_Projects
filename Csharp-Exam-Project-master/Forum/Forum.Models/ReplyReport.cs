namespace Forum.Models
{
    public class ReplyReport : Report
    {
        public Reply Reply { get; set; }

        public string ReplyId { get; set; }
    }
}