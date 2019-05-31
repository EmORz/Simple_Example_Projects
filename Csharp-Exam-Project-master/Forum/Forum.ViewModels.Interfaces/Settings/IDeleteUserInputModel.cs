using System.ComponentModel.DataAnnotations;

namespace Forum.ViewModels.Interfaces.Settings
{
    public interface IDeleteUserInputModel : IValidatableObject
    {
        string Username { get; set; }

        string Password { get; set; }
    }
}