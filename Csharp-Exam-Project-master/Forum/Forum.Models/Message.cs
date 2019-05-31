using System;

namespace Forum.Models
{
    public class Message : BaseEntity<string>
    {
        public string Description { get; set; }

        public ForumUser Author { get; set; }

        public string AuthorId { get; set; }

        public ForumUser Reciever { get; set; }

        public string RecieverId { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool Seen { get; set; }
    }
}