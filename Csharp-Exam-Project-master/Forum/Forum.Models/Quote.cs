using System;
using System.Collections.Generic;

namespace Forum.Models
{
    public class Quote : BaseEntity<string>
    {
        public Reply Reply { get; set; }

        public string ReplyId { get; set; }

        public DateTime QuotedOn { get; set; }

        public ForumUser Author { get; set; }

        public string AuthorId { get; set; }

        public string Description { get; set; }

        public ICollection<QuoteReport> Reports { get; set; }
    }
}