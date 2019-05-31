namespace Forum.ViewModels.Interfaces.Forum
{
    using System.Collections.Generic;
    using global::Forum.MapConfiguration.Contracts;
    using global::Forum.Models;

    public interface IForumPostsInputModel : IMapFrom<SubForum>
    {
        SubForum Forum { get; set; }

        IEnumerable<Post> Posts { get; set; }

        int PagesCount { get; }
    }
}