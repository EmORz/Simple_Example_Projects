using Forum.Services.Interfaces.Account;
using Forum.Services.Interfaces.Post;
using Forum.Services.Interfaces.Reply;
using Forum.ViewModels.Reply;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Forum.Web.Controllers.Reply
{
    public class ReplyController : BaseController
    {
        private readonly IReplyService replyService;
        private readonly IPostService postService;

        public ReplyController(IAccountService accountService, IReplyService replyService, IPostService postService)
            : base(accountService)
        {
            this.replyService = replyService;
            this.postService = postService;
        }

        [HttpPost]
        public IActionResult Create(ReplyInputModel model)
        {
            var post = this.postService.GetPost(model.PostId, 0, this.User, this.ModelState);

            if (ModelState.IsValid)
            {
                var user = this.accountService.GetUser(this.User);

                this.replyService.AddReply(model, user).GetAwaiter().GetResult();

                return this.Redirect($"/Post/Details?id={post.Id}");
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