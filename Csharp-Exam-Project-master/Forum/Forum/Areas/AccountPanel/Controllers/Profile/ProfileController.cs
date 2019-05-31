using Forum.Services.Common.Attributes.Validation;
using Forum.Services.Interfaces.Account;
using Forum.Services.Interfaces.Profile;
using Forum.Services.Interfaces.Settings;
using Forum.ViewModels.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Forum.Web.Areas.AccountPanel.Controllers.Profile
{
    [Area("AccountPanel")]
    public class ProfileController : Controller
    {
        private readonly IAccountService accountService;
        private readonly ISettingsService settingsService;
        private readonly IProfileService profileService;

        public ProfileController(IAccountService accountService, ISettingsService settingsService, IProfileService profileService)
        {
            this.accountService = accountService;
            this.settingsService = settingsService;
            this.profileService = profileService;
        }

        [Authorize]
        public IActionResult Details(string id)
        {
            var user = this.accountService.GetUserById(id, this.ModelState);

            if (this.ModelState.IsValid)
            {
                this.ViewData["profilePicUrl"] = user.ProfilePicutre;

                this.ViewData["userId"] = user.Id;

                this.ViewData["username"] = user.UserName;

                var model = this.profileService.GetProfileInfo(this.User);

                return this.View(model);
            }
            else
            {
                var result = this.View("Error", this.ModelState);
                result.StatusCode = (int)HttpStatusCode.NotFound;

                return result;
            }
        }

        [Authorize]
        public PartialViewResult MyProfile()
        {
            var model = this.profileService.GetProfileInfo(this.User);

            return this.PartialView("_MyProfilePartial", model);
        }

        [HttpPost("UploadProfilePicture")]
        public IActionResult UploadProfilePicture([AllowedImageExtensions] [Required(ErrorMessage = ErrorConstants.MustChooseAnImage)] IFormFile image)
        {
            if (ModelState.IsValid)
            {
                this.profileService.UploadProfilePicture(image, this.User.Identity.Name);
                return this.Redirect("/Account/Profile");
            }
            else
            {
                var result = this.View("Error", this.ModelState);
                result.StatusCode = (int)HttpStatusCode.BadRequest;

                return result;
            }
        }
    }
}