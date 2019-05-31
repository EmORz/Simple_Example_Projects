using Microsoft.AspNetCore.Mvc;
using Forum.Web.Controllers;
using System.Linq;
using Forum.Services.Interfaces.Category;
using Microsoft.AspNetCore.Http;
using System;
using Forum.Services.Interfaces.Post;
using Forum.Services.Interfaces.Account;
using Forum.ViewModels.Home;
using Forum.Services.Common;

namespace Forum.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ICategoryService categoryService;
        private readonly IPostService postService;

        public HomeController(IAccountService accountService, ICategoryService categoryService, IPostService postService) 
            : base(accountService)
        {
            this.categoryService = categoryService;
            this.postService = postService;
        }

        public IActionResult Index()
        {
            IndexInfoViewModel viewModel = new IndexInfoViewModel
            {
                Categories = this.categoryService.GetPublicCategories().ToArray(),
                TotalUsersCount = this.accountService.GetUsernames().Count(),
                NewestUser = this.accountService.GetNewestUser(),
                TotalPostsCount = this.postService.GetTotalPostsCount(),
                LatestPosts = this.postService.GetLatestPosts(this.User),
                PopularPosts = this.postService.GetPopularPosts(this.User)
            };

            if (this.User.IsInRole(Role.Administrator) || this.User.IsInRole(Role.Owner))
            {
                viewModel.Categories = this.categoryService.GetAllCategories();
            }

            return this.View(viewModel);
        }

        public IActionResult About()
        {
            return this.View();
        }
        
        public IActionResult AcceptConsent()
        {
            this.Response.Cookies.Append("GDPR", "true", new CookieOptions { Path = "/", Expires = DateTime.UtcNow.AddDays(3), HttpOnly = false, IsEssential = true });

            return this.Redirect("/");
        }

        public IActionResult Error()
        {
            return this.View();
        }
    }
}