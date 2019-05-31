using Forum.Services.Common;
using Forum.Services.Interfaces.Account;
using Forum.Services.Interfaces.Pagging;
using Forum.Services.Interfaces.Post;
using Forum.Services.Interfaces.Report.Post;
using Forum.ViewModels.Report;
using Forum.Web.Attributes.CustomAuthorizeAttributes;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Forum.Web.Controllers.PostReport
{ 
    public class PostReportController : BaseController
    {
        private readonly IPostService postService;
        private readonly IPostReportService postReportService;
        private readonly IPaggingService paggingService;

        public PostReportController(IAccountService accountService, IPostService postService, IPostReportService postReportService, IPaggingService paggingService) 
            : base(accountService)
        {
            this.postService = postService;
            this.postReportService = postReportService;
            this.paggingService = paggingService;
        }

        [HttpPost]
        public IActionResult Add(PostReportInputModel model)
        {
            var post = this.postService.GetPost(model.PostId, 0, this.User, this.ModelState);

            if (this.ModelState.IsValid)
            {
                string authorId = this.accountService.GetUser(this.User).Id;

                this.postReportService.AddPostReport(model, authorId);
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
        public PartialViewResult GetPostReports(int start)
        {
            var reports = this.postReportService.GetPostReports(start);

            this.ViewData["PostReportsCount"] = this.postReportService.GetPostReportsCount();

            this.ViewData["PagesCount"] = this.paggingService.GetPagesCount(this.postReportService.GetPostReportsCount());

            return this.PartialView("~/Views/Report/Post/_PostReportsPartial.cshtml", reports);
        }


        [AuthorizeRoles(Role.Administrator, Role.Owner)]
        public PartialViewResult DismissPostReport(string id)
        {
            this.postReportService.DismissPostReport(id, this.ModelState);
            if (this.ModelState.IsValid)
            {
                var reports = this.postReportService.GetPostReports(0);

                this.ViewData["PostReportsCount"] = this.postReportService.GetPostReportsCount();

                this.ViewData["PagesCount"] = this.paggingService.GetPagesCount(this.postReportService.GetPostReportsCount());

                return this.PartialView("~/Views/Report/Post/_PostReportsPartial.cshtml", reports);
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