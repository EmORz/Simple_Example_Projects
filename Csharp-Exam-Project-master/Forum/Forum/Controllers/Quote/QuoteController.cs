using Forum.Services.Interfaces.Account;
using Forum.Services.Interfaces.Post;
using Forum.Services.Interfaces.Quote;
using Forum.Services.Interfaces.Reply;
using Forum.ViewModels;
using Forum.ViewModels.Quote;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Forum.Web.Controllers.Quote
{
    [Authorize]
    public class QuoteController : BaseController
    {
        private readonly IQuoteService quoteService;
        private readonly IPostService postService;
        private readonly IReplyService replyService;

        public QuoteController(IAccountService accountService, IQuoteService quoteService, IPostService postService, IReplyService replyService) : base(accountService)
        {
            this.quoteService = quoteService;
            this.postService = postService;
            this.replyService = replyService;
        }

        public IActionResult Create(string id)
        {
            var reply = this.replyService.GetReply(id, this.ModelState);

            if (this.ModelState.IsValid)
            {
                var recieverName = reply.Author.UserName;

                var model =
                    new QuoteInputModel
                    {
                        ReplyId = id,
                        Quote = reply.Description,
                        RecieverId = reply.Author.Id,
                    };

                this.ViewData["ReplierName"] = reply.Author.UserName;
                this.ViewData["PostName"] = reply.Post.Name;

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
        public IActionResult Create(QuoteInputModel model)
        {
            var reply = this.replyService.GetReply(model.ReplyId, this.ModelState);

            if (this.ModelState.IsValid)
            {
                var user = this.accountService.GetUser(this.User);

                var recieverName = this.accountService.GetUserById(model.RecieverId, this.ModelState).UserName;

                model.Description = this.postService.ParseDescription(model.Description);

                this.quoteService.AddQuote(model, user, recieverName);

                return this.Redirect($"/Post/Details?id={reply.PostId}");
            }
            else
            {
                var result = this.View("Error", this.ModelState);
                result.StatusCode = (int)HttpStatusCode.BadRequest;

                return result;
            }
        }

        public IActionResult Quote(string id)
        {
            var quote = this.quoteService.GetQuote(id, this.ModelState);

            if (this.ModelState.IsValid)
            {
                var recieverName = quote.Author.UserName;

                var model =
                    new QuoteInputModel
                    {
                        ReplyId = quote.ReplyId,
                        Quote = quote.Description,
                        RecieverId = quote.Reply.Author.Id,
                    };

                this.ViewData["ReplierName"] = quote.Reply.Author.UserName;
                this.ViewData["PostName"] = quote.Reply.Post.Name;
                this.ViewData["QuoteRecieverId"] = quote.Author.Id;

                return this.View("QuoteAQuoteCreate", model);

            }
            else 
            {
                var result = this.View("Error", this.ModelState);
                result.StatusCode = (int)HttpStatusCode.NotFound;

                return result;
            }
        }

        [HttpPost]
        public IActionResult QuoteAQuoteCreate(QuoteInputModel model)
        {
            var reply = this.replyService.GetReply(model.ReplyId, this.ModelState);

            if (this.ModelState.IsValid)
            {
                var user = this.accountService.GetUser(this.User);
                
                var recieverName = this.accountService.GetUserById(model.QuoteRecieverId, this.ModelState).UserName;

                model.Description = this.postService.ParseDescription(model.Description);

                this.quoteService.AddQuote(model, user, recieverName);

                return this.Redirect($"/Post/Details?id={reply.PostId}");
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