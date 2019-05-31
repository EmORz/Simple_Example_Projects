using Forum.Services.Common;
using Forum.Services.Interfaces.Account;
using Forum.Services.Interfaces.Pagging;
using Forum.Services.Interfaces.Post;
using Forum.Services.Interfaces.Report.Quote;
using Forum.ViewModels.Report;
using Forum.Web.Attributes.CustomAuthorizeAttributes;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Forum.Web.Controllers.QuoteReport
{
    public class QuoteReportController : BaseController
    {
        private readonly IPostService postService;
        private readonly IQuoteReportService quoteReportService;
        private readonly IPaggingService paggingService;

        public QuoteReportController(IAccountService accountService, IPostService postService, IQuoteReportService quoteReportService, IPaggingService paggingService) 
            : base(accountService)
        {
            this.postService = postService;
            this.quoteReportService = quoteReportService;
            this.paggingService = paggingService;
        }

        [AuthorizeRoles(Role.Administrator, Role.Owner)]
        public PartialViewResult GetQuoteReports(int start)
        {
            var reports = this.quoteReportService.GetQuoteReports(start);

            this.ViewData["QuoteReportsCount"] = this.quoteReportService.GetQuoteReportsCount();

            this.ViewData["PagesCount"] = this.paggingService.GetPagesCount(this.quoteReportService.GetQuoteReportsCount());

            return this.PartialView("~/Views/Report/Quote/_QuoteReportsPartial.cshtml", reports);
        }

        [AuthorizeRoles(Role.Administrator, Role.Owner)]
        public PartialViewResult DismissQuoteReport(string id)
        {
            this.quoteReportService.DismissQuoteReport(id, this.ModelState);

            if (this.ModelState.IsValid)
            {
                var reports = this.quoteReportService.GetQuoteReports(0);

                this.ViewData["QuoteReportsCount"] = this.quoteReportService.GetQuoteReportsCount();

                this.ViewData["PagesCount"] = this.paggingService.GetPagesCount(this.quoteReportService.GetQuoteReportsCount());

                return this.PartialView("~/Views/Report/Quote/_QuoteReportsPartial.cshtml", reports);
            }
            else
            {
                var result = this.PartialView("_ErrorPartial", this.ModelState);
                result.StatusCode = (int)HttpStatusCode.NotFound;

                return result;
            }
        }

        [HttpPost]
        public IActionResult Add(QuoteReportInputModel model)
        {
            var post = this.postService.GetPost(model.PostId, 0, this.User, this.ModelState);

            if (ModelState.IsValid)
            {
                string authorId = this.accountService.GetUser(this.User).Id;

                this.quoteReportService.AddQuoteReport(model, authorId);
                return this.Redirect($"/Post/Details?id={post.Id}");
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