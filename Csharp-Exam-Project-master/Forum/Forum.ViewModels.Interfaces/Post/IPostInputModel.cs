using Forum.MapConfiguration.Contracts;

namespace Forum.ViewModels.Interfaces.Post
{
    public interface IPostInputModel : IMapTo<Models.Post>
    {
        string Name { get; set; }

        string Description { get; set; }

        string ForumId { get; set; }

        string ForumName { get; set; }
    }
}