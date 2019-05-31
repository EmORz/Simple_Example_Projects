using System.Collections.Generic;
using Forum.MapConfiguration.Contracts;
using Forum.Models;
using Forum.ViewModels.Interfaces.Forum;

namespace Forum.ViewModels.Forum
{
    public class ForumPostsInputModel : IForumPostsInputModel, IMapFrom<SubForum>
    {
        public SubForum Forum { get; set; }

        public IEnumerable<Models.Post> Posts { get; set; }

        public int PagesCount { get; set; }
    }
}