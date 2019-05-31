using Forum.Services.Interfaces.Account;
using Forum.Services.Interfaces.Settings;
using Forum.ViewModels.Common;
using Forum.ViewModels.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Forum.Web.Areas.AccountPanel.Controllers.Settings
{
    [Area("AccountPanel")]
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly ISettingsService settingsService;
        private readonly IAccountService accountService;

        public SettingsController(ISettingsService settingsService, IAccountService accountService)
        {
            this.settingsService = settingsService;
            this.accountService = accountService;
        }

        public IActionResult DownloadInfo()
        {
            var byteArr = this.settingsService.BuildFile(this.User);

            return this.File(byteArr, "text/json", "info.txt");
        }

        public PartialViewResult Settings()
        {
            return this.PartialView("_SettingsPartial");
        }

        public PartialViewResult EditProfile()
        {
            var model = this.settingsService.MapEditModel(this.User.Identity.Name);

            return this.PartialView("_EditProfilePartial", model);
        }

        [HttpPost("EditProfile")]
        public IActionResult EditProfile(EditProfileInputModel model)
        {
            var user = this.accountService.GetUserByName(this.User.Identity.Name, this.ModelState);

            var passwordCheck = this.settingsService.CheckPassword(user, model.Password, this.ModelState);

            if (ModelState.IsValid)
            {
                this.accountService.LogoutUser();

                this.settingsService.EditProfile(user, model);

                return this.Redirect("/");
            }
            else
            {
                var result = this.View("Error", ModelState);
                result.StatusCode = (int)HttpStatusCode.BadRequest;

                return result;
            }
        }

        public PartialViewResult ChangePassword()
        {
            return this.PartialView("_ChangePasswordPartial");
        }

        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordInputModel model)
        {
            if (ModelState.IsValid)
            {
                var user = this.accountService.GetUser(this.User);

                this.settingsService.ChangePassword(user, model.OldPassword, model.NewPassword);

                this.accountService.LogoutUser();

                return this.Redirect("/");
            }
            else
            {
                var result = this.View("Error", ModelState);
                result.StatusCode = (int)HttpStatusCode.BadRequest;

                return result;
            }
        }

        public PartialViewResult DeleteAccount()
        {
            return this.PartialView("_DeleteAccountPartial");
        }

        [HttpPost("DeleteAccount")]
        public IActionResult DeleteAccount(DeleteUserInputModel model)
        {
            var user = this.accountService.GetUserByName(model.Username, this.ModelState);
            if (this.User.Identity.Name != model.Username)
            {
                this.ModelState.AddModelError("error", ErrorConstants.IncorrectUsernameOrPasswordError);
            }

            if (ModelState.IsValid)
            {
                this.accountService.LogoutUser();

                this.settingsService.DeleteAccount(user);

                return this.Redirect("/");
            }
            else
            {
                var result = this.View("Error", ModelState);
                result.StatusCode = (int)HttpStatusCode.NotFound;

                return result;
            }
        }
    }
}