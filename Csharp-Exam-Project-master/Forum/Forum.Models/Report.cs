using System;

namespace Forum.Models
{
    public abstract class Report : BaseEntity<string>
    {
        public ForumUser Author { get; set; }

        public string Title { get; set; }

        public string AuthorId { get; set; }

        public string Description { get; set; }

        public DateTime ReportedOn { get; set; }
    }
}