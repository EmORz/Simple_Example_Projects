using Forum.Models;
using Forum.ViewModels.Interfaces.Settings;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Security.Claims;

namespace Forum.Services.Interfaces.Settings
{
    public interface ISettingsService
    {
        void ChangePassword(ForumUser user, string oldPassword, string newPassword);

        bool CheckPassword(ForumUser user, string password, ModelStateDictionary modelState);

        bool DeleteAccount(ForumUser user);

        IEditProfileInputModel MapEditModel(string username);

        bool EditProfile(ForumUser user, IEditProfileInputModel model);

        byte[] BuildFile(ClaimsPrincipal principal);
    }
}