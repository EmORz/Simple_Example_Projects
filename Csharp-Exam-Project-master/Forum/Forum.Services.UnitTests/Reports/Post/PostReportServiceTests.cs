using AutoMapper;
using Forum.Models;
using Forum.Services.Common;
using Forum.Services.Interfaces.Db;
using Forum.Services.Interfaces.Report.Post;
using Forum.Services.UnitTests.Base;
using Forum.ViewModels.Report;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using Xunit;

namespace Forum.Services.UnitTests.Report.Post
{
    public class ReplyReportServiceTests : IClassFixture<BaseUnitTest>
    {
        private readonly IDbService dbService;

        private readonly IMapper mapper;

        private readonly IPostReportService postReportService;

        public ReplyReportServiceTests(BaseUnitTest fixture)
        {
            this.dbService = fixture.Provider.GetService(typeof(IDbService)) as IDbService;

            this.mapper = fixture.Provider.GetService(typeof(IMapper)) as IMapper;

            this.postReportService = fixture.Provider.GetService(typeof(IPostReportService)) as IPostReportService;

            this.SeedDb();
        }

        private void TruncateUsersTable()
        {
            var users = this.dbService.DbContext.Users.ToList();
            this.dbService.DbContext.Users.RemoveRange(users);

            this.dbService.DbContext.SaveChanges();
        }

        private void TruncatePostsTable()
        {
            var posts = this.dbService.DbContext.Posts.ToList();
            this.dbService.DbContext.Posts.RemoveRange(posts);

            this.dbService.DbContext.SaveChanges();
        }

        private void TruncatePostReportsTable()
        {
            var postReports = this.dbService.DbContext.PostReports.ToList();
            this.dbService.DbContext.PostReports.RemoveRange(postReports);

            this.dbService.DbContext.SaveChanges();
        }

        private void SeedDb()
        {
            this.TruncatePostReportsTable();
            this.TruncatePostsTable();
            this.TruncateUsersTable();

            var author = new ForumUser { Id = TestsConstants.TestId };
            this.dbService.DbContext.Users.Add(author);
            this.dbService.DbContext.SaveChanges();
            
            var post = new Models.Post { Id = TestsConstants.TestId1, Author = author, AuthorId = author.Id };
            this.dbService.DbContext.Posts.Add(post);
            this.dbService.DbContext.SaveChanges();

            var firstPostReport = new PostReport { Id = TestsConstants.TestId, Author = author, AuthorId = author.Id };
            this.dbService.DbContext.PostReports.Add(firstPostReport);
            this.dbService.DbContext.SaveChanges();

            for (int i = 0; i < 5; i++)
            {
                var postReport = new PostReport();

                this.dbService.DbContext.PostReports.Add(postReport);
                this.dbService.DbContext.SaveChanges();
            }
        }

        [Fact]
        public void GetPostReportsCount_returns_correct_result()
        {
            var expectedResult = 6;

            var actualResult = this.postReportService.GetPostReportsCount();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void DismissPostReport_returns_correct_result()
        {
            var expectedResult = 1;

            var actualResult = this.postReportService.DismissPostReport(TestsConstants.TestId, new ModelStateDictionary());

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void DismissPostReport_returns_zero_results_when_id_is_invalid()
        {
            var expectedResult = 0;

            var actualResult = this.postReportService.DismissPostReport(Guid.NewGuid().ToString(), new ModelStateDictionary());

            Assert.Equal(expectedResult, actualResult);

            this.SeedDb();
        }

        [Fact]
        public void GetPostReports_returns_list_when_correct()
        {
            var expectedResult = 5;

            var actualResult = this.postReportService.GetPostReports(0);

            Assert.Equal(expectedResult, actualResult.Count());
        }

        [Fact]
        public void AddPostReport_returns_correct_result()
        {
            var model = new PostReportInputModel { Description = TestsConstants.TestDescription, PostId = TestsConstants.TestId1, Title = TestsConstants.TestTitle };

            var expectedResult = model;

            var actualResult = this.postReportService.AddPostReport(model, TestsConstants.TestId);

            Assert.Equal(expectedResult, actualResult);
        }

    }
}