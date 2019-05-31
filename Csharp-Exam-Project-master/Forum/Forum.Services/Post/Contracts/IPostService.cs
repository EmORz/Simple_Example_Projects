namespace Forum.Services.Post.Contracts
{
    using global::Forum.Models;

    public interface IPostService
    {
        void AddPost(Post model, ForumUser user, string forumId);
    }
}