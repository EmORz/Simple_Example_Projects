using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Forum.ViewModels.Interfaces.Post
{
    public interface IEditPostInputModel
    {
        string Id { get; }

        string Name { get; }

        string Description { get; }

        string ForumName { get; }

        string ForumId { get; }

        IEnumerable<Models.SubForum> AllForums { get; }
    }
}