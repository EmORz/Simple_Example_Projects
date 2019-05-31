using Forum.MapConfiguration.Contracts;

namespace Forum.ViewModels.Interfaces.Settings
{
    public interface IEditProfileInputModel : IHaveCustomMappings
    {
        string Username { get; set; }

        string Location { get; set; }

        string Password { get; set; }

        string ConfirmPassword { get; set; }

        string Gender { get; set; }
    }
}