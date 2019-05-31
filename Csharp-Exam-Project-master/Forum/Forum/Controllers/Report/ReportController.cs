using Forum.Services.Common;
using Forum.Services.Interfaces.Account;
using Forum.Services.Interfaces.Pagging;
using Forum.Services.Interfaces.Post;
using Forum.Services.Interfaces.Report;
using Forum.Services.Interfaces.Report.Post;
using Forum.Services.Interfaces.Report.Quote;
using Forum.Services.Interfaces.Report.Reply;
using Forum.Web.Attributes.CustomAuthorizeAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Forum.Web.Controllers.Report
{
    [Authorize]
    public class ReportController : BaseController
    {
        public ReportController(IAccountService accountService)
            : base(accountService)
        {
        }

        [AuthorizeRoles(Role.Administrator, Role.Owner)]
        public IActionResult All()
        {
            return this.View();
        }
    }
}