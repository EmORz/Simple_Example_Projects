namespace Forum.ViewModels.Interfaces.Account
{
    public interface ILoginUserInputModel
    {
        string Username { get; set; }
        
        string Password { get; set; }
    }
}