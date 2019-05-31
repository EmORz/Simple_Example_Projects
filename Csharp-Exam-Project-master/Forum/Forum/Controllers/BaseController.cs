namespace Forum.Web.Controllers
{
    using global::Forum.Services.Interfaces.Account;
    using Microsoft.AspNetCore.Mvc;

    public class BaseController : Controller
    {
        protected readonly IAccountService accountService;

        public BaseController(IAccountService accountService)
        {
            this.accountService = accountService;
        }
    }
}