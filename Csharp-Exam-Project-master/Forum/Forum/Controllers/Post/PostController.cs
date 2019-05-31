using Forum.Models;
using Forum.Services.Interfaces.Account;
using Forum.Services.Interfaces.Forum;
using Forum.Services.Interfaces.Pagging;
using Forum.Services.Interfaces.Post;
using Forum.Services.Interfaces.Quote;
using Forum.Services.Interfaces.Reply;
using Forum.ViewModels.Common;
using Forum.ViewModels.Post;
using Forum.Web.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;

namespace Forum.Web.Controllers.Post
{
    [Authorize]
    public class PostController : BaseController
    {
        private readonly IPaggingService paggingService;
        private readonly IReplyService replyService;
        private readonly IQuoteService quoteService;
        private readonly IForumService forumService;
        private readonly IPostService postService;

        public PostController(IPaggingService paggingService, IAccountService accountService, IReplyService replyService, IQuoteService quoteService, IForumService forumService, IPostService postService)
            : base(accountService)
        {
            this.paggingService = paggingService;
            this.replyService = replyService;
            this.quoteService = quoteService;
            this.forumService = forumService;
            this.postService = postService;
        }

        public IActionResult Create(string id)
        {
            var Forum = this.forumService.GetForum(id, this.ModelState);

            if (this.ModelState.IsValid)
            {
                PostInputModel model = new PostInputModel
                {
                    ForumId = Forum.Id,
                    ForumName = Forum.Name
                };

                return this.View(model);
            }
            else
            {
                var result = this.View("Error", this.ModelState);
                result.StatusCode = (int)HttpStatusCode.BadRequest;

                return result;
            }
        }

        [HttpPost]
        public IActionResult Create(PostInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                ForumUser user = this.accountService.GetUser(this.User);
                this.postService.AddPost(model, user, model.ForumId).GetAwaiter().GetResult();

                return this.Redirect($"/Forum/Posts?Id={model.ForumId}");
            }
            else
            {
                var result = this.View("Error", this.ModelState);
                result.StatusCode = (int)HttpStatusCode.BadRequest;

                return result;
            }
        }

        [AllowAnonymous]
        [TypeFilter(typeof(ViewsFilter))]
        public IActionResult Details(string Id, int start)
        {
            var viewModel = this.postService.GetPost(Id, start, this.User, this.ModelState);
            if (this.ModelState.IsValid)
            {
                viewModel.PagesCount = this.paggingService.GetPagesCount(this.replyService.GetPostRepliesIds(Id).Count());

                this.ViewData["PostId"] = Id;

                this.ViewData["PostReplyIds"] = this.replyService.GetPostRepliesIds(Id).ToList();

                return this.View(viewModel);
            }
            else
            {
                var result = this.View("Error", this.ModelState);
                result.StatusCode = (int)HttpStatusCode.NotFound;

                return result;
            }
        }

        public IActionResult Edit(string Id)
        {
            var postExists = this.postService.GetPost(Id, default(int), this.User, this.ModelState);

            var viewModel = this.postService.GetEditPostModel(Id, this.User, this.ModelState);

            if (this.ModelState.IsValid)
            {
                return this.View(viewModel);
            }
            else
            {
                var result = this.View("Error", this.ModelState);
                result.StatusCode = (int)HttpStatusCode.NotFound;

                return result;
            }
        }

        [HttpPost]
        public IActionResult Edit(EditPostInputModel model)
        {
            var forumsIds = this.forumService.GetAllForumsIds(this.User, this.ModelState, model.ForumId);

            this.postService.Edit(model, this.User, this.ModelState);

            if (this.ModelState.IsValid)
            {
                return this.Redirect($"/Post/Details?Id={model.Id}");
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