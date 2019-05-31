using Forum.Services.Common;
using Forum.Services.Interfaces.Account;
using Forum.Services.Interfaces.Category;
using Forum.ViewModels.Category;
using Forum.Web.Attributes.CustomAuthorizeAttributes;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Forum.Web.Controllers.Category
{
    [AuthorizeRoles(Role.Administrator, Role.Owner)]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService categoryService;

        public CategoryController(IAccountService accountService, ICategoryService categoryService) 
            : base(accountService)
        {
            this.categoryService = categoryService;
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CategoryInputModel model)
        {
            if(ModelState.IsValid)
            {
                var user = this.accountService.GetUser(this.User);
                
                var result = this.categoryService.AddCategory(model, user).GetAwaiter().GetResult();

                return this.Redirect("/");
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