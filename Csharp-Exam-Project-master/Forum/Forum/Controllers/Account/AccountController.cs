using Forum.Services.Interfaces.Account;
using Forum.Services.Interfaces.Message;
using Forum.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Forum.Web.Controllers.Account
{
    public class AccountController : BaseController
    {
        private readonly IMessageService messageService;

        public AccountController(IAccountService accountService, IMessageService messageService) : base(accountService)
        {
            this.messageService = messageService;
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                this.Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserInputModel model)
        {
            if (ModelState.IsValid)
            {
                this.accountService.LoginUser(model);
                return this.Redirect("/");
            }
            else
            {
                var result = this.View("Error", this.ModelState);
                result.StatusCode = (int)HttpStatusCode.BadRequest;

                return result;
            }
        }

        public IActionResult Register()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                this.Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                this.accountService.RegisterUser(model);

                return this.Redirect("/");
            }
            else
            {
                var result = this.View("Error", this.ModelState);
                result.StatusCode = (int)HttpStatusCode.BadRequest;

                return result;
            }
        }

        [Authorize]
        public IActionResult Profile()
        {
            var user = this.accountService.GetUserByName(this.User.Identity.Name, this.ModelState);

            this.ViewData["profilePicUrl"] = user.ProfilePicutre ?? null;

            return this.View();
        }

        [Authorize]
        public IActionResult Dismiss()
        {
            this.accountService.LogoutUser();

            return this.Redirect("/Account/Logout");
        }

        [Authorize]
        public IActionResult Logout()
        {
            return this.View();
        }
    }
}