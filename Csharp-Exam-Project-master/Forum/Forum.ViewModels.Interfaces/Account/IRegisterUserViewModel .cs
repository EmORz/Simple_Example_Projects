namespace Forum.ViewModels.Interfaces.Account
{
    using Microsoft.AspNetCore.Http;

    public interface IRegisterUserViewModel 
    {
        string Username { get; set; }

        string Email { get; set; }

        string Location { get; set; }

        string Password { get; set; }

        string ConfirmPassword { get; set; }

        string Gender { get; set; }

        IFormFile Image { get; set; }
    }
}