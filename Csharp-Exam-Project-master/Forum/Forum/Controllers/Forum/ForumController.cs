using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Forum.Services.Interfaces.Category;
using Forum.Services.Interfaces.Forum;
using Forum.ViewModels.Forum;
using Forum.Web.Attributes.CustomAuthorizeAttributes;
using Forum.Services.Interfaces.Db;
using System.Net;
using Forum.Services.Interfaces.Account;
using Forum.Services.Common;
using Forum.Services.Interfaces.Pagging;

namespace Forum.Web.Controllers.Forum
{
    [AuthorizeRoles(Role.Administrator, Role.Owner)]
    public class ForumController : BaseController
    {
        private readonly IPaggingService paggingService;
        private readonly IDbService dbService;
        private readonly ICategoryService categoryService;
        private readonly IForumService forumService;

        public ForumController(IPaggingService paggingService, IDbService dbService, IAccountService accountService, ICategoryService categoryService, IForumService forumService)
            : base(accountService)
        {
            this.paggingService = paggingService;
            this.dbService = dbService;
            this.categoryService = categoryService;
            this.forumService = forumService;
        }

        public IActionResult Create()
        {
            var names = this.categoryService.GetAllCategories();

            var namesList =
                names
                .Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Name
                })
                .ToList();

            ForumFormInputModel model = new ForumFormInputModel
            {
                Categories = namesList
            };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Create(ForumFormInputModel model)
        {
            if (ModelState.IsValid)
            {
                this.forumService.AddForum(model, model.ForumModel.Category);

                return this.Redirect("/");
            }
            else
            {
                var result = this.View("Error", ModelState);
                result.StatusCode = (int)HttpStatusCode.BadRequest;

                return result;
            }
        }

        [AllowAnonymous]
        public IActionResult Posts(string id, int start)
        {
            var forum = this.forumService.GetForum(id, this.ModelState);

            if (this.ModelState.IsValid)
            {
                var posts = this.forumService.GetPostsByForum(forum.Id, start);

                this.ViewData["PostsIds"] = this.forumService.GetForumPostsIds(id);

                var model = new ForumPostsInputModel
                {
                    Forum = forum,
                    Posts = posts,
                    PagesCount = this.paggingService.GetPagesCount(forum.Posts.Count())
                };

                return this.View(model);
            }
            else
            {
                var result = this.View("Error", this.ModelState);
                result.StatusCode = (int)HttpStatusCode.NotFound;

                return result;
            }
        }

        public IActionResult Edit(string id)
        {
            var forum = this.forumService.GetForum(id, this.ModelState);
            
            if (this.ModelState.IsValid)
            {
                var model = (ForumFormInputModel)this.forumService.GetMappedForumModel(forum);

                this.ViewData["ForumId"] = forum.Id;

                return this.View(model);
            }
            else
            {
                var result = this.View("Error", this.ModelState);
                result.StatusCode = (int)HttpStatusCode.NotFound;

                return result;
            }
        }

        [HttpPost]
        public IActionResult Edit(EditForumFormInputModel model, string forumId)
        {
            var forum = this.forumService.GetForum(forumId, this.ModelState);
            
            if (this.ModelState.IsValid)
            {
                this.forumService.Edit(model.ForumModel, forumId);

                return this.Redirect("/");
            }
            else
            {
                var result = this.View("Error", this.ModelState);
                result.StatusCode = (int)HttpStatusCode.NotFound;

                return result;
            }
        }

        public IActionResult Delete(string id)
        {
            var forum = this.forumService.GetForum(id, this.ModelState);
            
            if (this.ModelState.IsValid)
            {
                this.forumService.Delete(forum);

                return this.Redirect("/");
            }
            else
            {
                var result = this.View("Error", this.ModelState);
                result.StatusCode = (int)HttpStatusCode.NotFound;

                return result;
            }
        }
    }
}