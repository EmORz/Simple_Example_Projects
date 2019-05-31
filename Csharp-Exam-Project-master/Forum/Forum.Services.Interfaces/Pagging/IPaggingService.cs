namespace Forum.Services.Interfaces.Pagging
{
    public interface IPaggingService
    {
        int GetPagesCount(int postsCount);
    }
}