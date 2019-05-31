namespace Forum.Services.Account.Contracts
{
    using global::Forum.Models;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public interface IAccountService
    {
        int GetUsersCount();

        bool UsernameExists(string username);

        int GetTotalPostsCount();

        ForumUser GetUser(ClaimsPrincipal principal);

        void LoginUser(ForumUser model, string password);

        void LogoutUser();

        string GetNewestUser();

        Task<bool> EmailExists(string email);

        bool UserExists(string username);

        Task<bool> UserWithPasswordExists(string username, string password);

        void RegisterUser(ForumUser model, string password);
    }
}