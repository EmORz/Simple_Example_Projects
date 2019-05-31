using Forum.ViewModels.Interfaces.Profile;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Forum.Services.Interfaces.Profile
{
    public interface IProfileService
    {
        IProfileInfoViewModel GetProfileInfo(ClaimsPrincipal principal);

        bool IsImageExtensionValid(string fileName);

        void UploadProfilePicture(IFormFile image, string username);
    }
}