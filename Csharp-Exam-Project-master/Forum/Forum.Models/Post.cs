using System;
using System.Collections.Generic;

namespace Forum.Models
{
    public class Post : BaseEntity<string>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartedOn { get; set; }

        public ForumUser Author { get; set; }

        public string AuthorId { get; set; }

        public int Views { get; set; }

        public SubForum Forum { get; set; }

        public string ForumId { get; set; }

        public ICollection<Reply> Replies { get; set; }

        public ICollection<PostReport> Reports { get; set; }
    }
}