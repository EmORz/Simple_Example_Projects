using Forum.MapConfiguration.Contracts;
using Forum.Models;

namespace Forum.ViewModels.Interfaces.Reply
{
    public interface IReplyInputModel : IMapTo<Models.Reply>
    {
        string Id { get; set; }

        ForumUser Author { get; set; }

        string Description { get; set; }

        string PostId { get; set; }
    }
}