using AutoMapper;
using Forum.Models;
using Forum.Services.Common;
using Forum.Services.Interfaces.Db;
using Forum.Services.Interfaces.Report;
using Forum.Services.UnitTests.Base;
using System.Linq;
using Xunit;

namespace Forum.Services.UnitTests.Reports
{
    public class ReportsServiceTests : IClassFixture<BaseUnitTest>
    {
        private readonly IDbService dbService;

        private readonly IMapper mapper;

        private readonly IReportService reportService;

        public ReportsServiceTests(BaseUnitTest fixture)
        {
            this.dbService = fixture.Provider.GetService(typeof(IDbService)) as IDbService;

            this.mapper = fixture.Provider.GetService(typeof(IMapper)) as IMapper;

            this.reportService = fixture.Provider.GetService(typeof(IReportService)) as IReportService;

            this.SeedDb();
        }

        private void TruncateUsersTable()
        {
            var users = this.dbService.DbContext.Users.ToList();
            this.dbService.DbContext.Users.RemoveRange(users);

            this.dbService.DbContext.SaveChanges();
        }

        private void TruncatePostReportsTable()
        {
            var postReports = this.dbService.DbContext.PostReports.ToList();
            this.dbService.DbContext.PostReports.RemoveRange(postReports);

            this.dbService.DbContext.SaveChanges();
        }

        private void TruncateQuoteReportsTable()
        {
            var quoteReports = this.dbService.DbContext.QuoteReports.ToList();
            this.dbService.DbContext.QuoteReports.RemoveRange(quoteReports);

            this.dbService.DbContext.SaveChanges();
        }
        
        private void TruncateReplyReportsTable()
        {
            var replyReports = this.dbService.DbContext.ReplyReports.ToList();
            this.dbService.DbContext.ReplyReports.RemoveRange(replyReports);

            this.dbService.DbContext.SaveChanges();
        }

        private void SeedDb()
        {
            this.TruncatePostReportsTable();
            this.TruncateReplyReportsTable();
            this.TruncateUsersTable();
            this.TruncateQuoteReportsTable();

            var user = new ForumUser { Id = TestsConstants.TestId, UserName = TestsConstants.TestUsername1 };
            this.dbService.DbContext.Users.Add(user);
            this.dbService.DbContext.SaveChanges();

            var replyReport = new ReplyReport { Author = user, AuthorId = user.Id };
            var postReport = new PostReport { Author = user, AuthorId = user.Id };
            var quoteReport = new QuoteReport { Author = user, AuthorId = user.Id };

            this.dbService.DbContext.ReplyReports.Add(replyReport);
            this.dbService.DbContext.PostReports.Add(postReport);
            this.dbService.DbContext.QuoteReports.Add(quoteReport);
            this.dbService.DbContext.SaveChanges();

        }

        [Fact]
        public void DeleteUserReports_returns_correct_result_when_correct()
        {
            var expectedResult = 3;

            var actualResult = this.reportService.DeleteUserReports(TestsConstants.TestUsername1);

            Assert.Equal(expectedResult, actualResult);
        }

    }
}