using System;
using System.Collections.Generic;

namespace Forum.Models
{
    public class SubForum
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public ICollection<Post> Posts { get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        public string CategoryId { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
