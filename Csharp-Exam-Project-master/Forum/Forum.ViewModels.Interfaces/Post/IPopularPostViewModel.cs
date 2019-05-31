namespace Forum.ViewModels.Interfaces.Post
{
    public interface IPopularPostViewModel
    {
        string Id { get; }

        string Name { get; }

        int Views { get; }
    }
}