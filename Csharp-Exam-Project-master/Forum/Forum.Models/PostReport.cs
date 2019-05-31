namespace Forum.Models
{
    public class PostReport : Report
    {
        public Post Post { get; set; }

        public string PostId { get; set; }
    }
}