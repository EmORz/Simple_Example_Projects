using Forum.Services.Interfaces.Account;
using Forum.Services.Interfaces.Pagging;
using Forum.Services.Interfaces.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace Forum.Web.Areas.Owner.Controllers.Role
{
    [Area("Owner")]
    [Authorize(Forum.Services.Common.Role.Owner)]
    [Route("[area]/[controller]")]
    public class RoleController : Controller
    {
        private readonly IRoleService roleService;
        private readonly IAccountService accountService;
        private readonly IPaggingService paggingService;

        public RoleController(IRoleService roleService, IAccountService accountService, IPaggingService paggingService)
        {
            this.roleService = roleService;
            this.accountService = accountService;
            this.paggingService = paggingService;
        }

        [HttpGet("Index")]
        public IActionResult Index(int start)
        {
            var usersRoles = this.roleService.GetUsersRoles(start);

            this.ViewData["usernames"] = this.accountService.GetUsernamesWithoutOwner();

            this.ViewData["pagesCount"] = this.paggingService.GetPagesCount(this.accountService.GetUsernamesWithoutOwner().Count());

            return this.View(usersRoles);
        }

        [HttpGet("Promote/id={id}")]
        public IActionResult Promote(string id)
        {
            var user = this.accountService.GetUserById(id, this.ModelState);

            if (this.ModelState.IsValid)
            {
                this.roleService.Promote(user);

                return this.Redirect($"/Owner/Role/Index");
            }
            else
            {
                var result = this.View("Error", this.ModelState);
                result.StatusCode = (int)HttpStatusCode.NotFound;

                return result;
            }
        }

        [HttpGet("Demote/id={id}")]
        public IActionResult Demote(string id)
        {
            var user = this.accountService.GetUserById(id, this.ModelState);

            if (this.ModelState.IsValid)
            {
                this.roleService.Demote(user);
                
                return this.Redirect($"/Owner/Role/Index");
            }
            else
            {
                var result = this.View("Error", this.ModelState);
                result.StatusCode = (int)HttpStatusCode.NotFound;

                return result;
            }
        }

        [HttpGet("Search")]
        public PartialViewResult Search(string key)
        {
            var usersRoles = this.roleService.SearchForUsers(key);

            return this.PartialView("_EditRolesTablePartial", usersRoles);
        }
    }
}