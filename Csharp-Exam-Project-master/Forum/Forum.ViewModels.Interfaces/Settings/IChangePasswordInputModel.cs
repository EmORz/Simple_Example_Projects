using System.ComponentModel.DataAnnotations;

namespace Forum.ViewModels.Interfaces.Settings
{
    public interface IChangePasswordInputModel : IValidatableObject
    {
        string Username { get; set; }

        string OldPassword { get; set; }

        string NewPassword { get; set; }
    }
}