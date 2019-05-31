using Forum.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Forum.Models
{
    public class Category : BaseEntity<string>
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public string Name { get; set; }

        public ForumUser User { get; set; }
        
        public string UserId { get; set; }

        public ICollection<SubForum> Forums { get; set; }

        public CategoryType Type { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}