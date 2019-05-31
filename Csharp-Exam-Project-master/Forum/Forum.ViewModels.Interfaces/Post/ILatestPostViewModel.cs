namespace Forum.ViewModels.Interfaces.Post
{
    public interface ILatestPostViewModel
    {
        string Id { get; }

        string Name { get; }

        string AuthorName { get; }

        string StartedOn { get; }
    }
}