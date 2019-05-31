using System;
using System.Collections.Generic;

namespace Forum.Models
{
    public class Reply : BaseEntity<string>
    {
        public ForumUser Author { get; set; }

        public string AuthorId { get; set; }

        public string Description { get; set; }

        public Post Post { get; set; }

        public string PostId { get; set; }

        public ICollection<Quote> Quotes { get; set; }

        public DateTime RepliedOn { get; set; }

        public ICollection<ReplyReport> Reports { get; set; }
    }
}