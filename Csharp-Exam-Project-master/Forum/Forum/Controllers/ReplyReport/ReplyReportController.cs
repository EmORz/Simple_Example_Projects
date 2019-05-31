using Forum.Services.Common;
using Forum.Services.Interfaces.Account;
using Forum.Services.Interfaces.Pagging;
using Forum.Services.Interfaces.Post;
using Forum.Services.Interfaces.Report.Reply;
using Forum.ViewModels.Report;
using Forum.Web.Attributes.CustomAuthorizeAttributes;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Forum.Web.Controllers.ReplyReport
{
    public class ReplyReportController : BaseController
    {
        private readonly IPostService postService;
        private readonly IReplyReportService replyReportService;
        private readonly IPaggingService paggingService;

        public ReplyReportController(IAccountService accountService, IPostService postService, IReplyReportService replyReportService, IPaggingService paggingService)
            : base(accountService)
        {
            this.postService = postService;
            this.replyReportService = replyReportService;
            this.paggingService = paggingService;
        }

        [HttpPost]
        public IActionResult Add(ReplyReportInputModel model)
        {
            var post = this.postService.GetPost(model.PostId, 0, this.User, this.ModelState);

            if (ModelState.IsValid)
            {
                string authorId = this.accountService.GetUser(this.User).Id;

                this.replyReportService.AddReplyReport(model, authorId);
                return this.Redirect($"/Post/Details?id={post.Id}");
            }
            else
            {
                var result = this.View("Error", this.ModelState);
                result.StatusCode = (int)HttpStatusCode.BadRequest;

                return result;
            }
        }

        [AuthorizeRoles(Role.Administrator, Role.Owner)]
        public PartialViewResult GetReplyReports(int start)
        {
            var reports = this.replyReportService.GetReplyReports(start);

            this.ViewData["ReplyReportsCount"] = this.replyReportService.GetReplyReportsCount();

            this.ViewData["PagesCount"] = this.paggingService.GetPagesCount(this.replyReportService.GetReplyReportsCount());

            return this.PartialView("~/Views/Report/Reply/_ReplyReportsPartial.cshtml", reports);
        }

        [AuthorizeRoles(Role.Administrator, Role.Owner)]
        public PartialViewResult DismissReplyReport(string id)
        {
            this.replyReportService.DismissReplyReport(id, this.ModelState);
            if (this.ModelState.IsValid)
            {
                var reports = this.replyReportService.GetReplyReports(0);

                this.ViewData["ReplyReportsCount"] = this.replyReportService.GetReplyReportsCount();

                this.ViewData["PagesCount"] = this.paggingService.GetPagesCount(this.replyReportService.GetReplyReportsCount());

                return this.PartialView("~/Views/Report/Reply/_ReplyReportsPartial.cshtml", reports);
            }
            else
            {
                var result = this.PartialView("_ErrorPartial", this.ModelState);
                result.StatusCode = (int)HttpStatusCode.NotFound;

                return result;
            }
        }

    }
}