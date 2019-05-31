using AutoMapper;
using Forum.Models;
using Forum.Services.Common;
using Forum.Services.Interfaces.Db;
using Forum.Services.Interfaces.Quote;
using Forum.Services.UnitTests.Base;
using Forum.ViewModels.Quote;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Linq;
using Xunit;

namespace Forum.Services.UnitTests.Quote
{
    public class QuoteServiceTests : IClassFixture<BaseUnitTest>
    {
        private readonly IDbService dbService;

        private readonly IMapper mapper;

        private readonly IQuoteService quoteService;

        public QuoteServiceTests(BaseUnitTest fixture)
        {
            this.dbService = fixture.Provider.GetService(typeof(IDbService)) as IDbService;

            this.mapper = fixture.Provider.GetService(typeof(IMapper)) as IMapper;

            this.quoteService = fixture.Provider.GetService(typeof(IQuoteService)) as IQuoteService;

            this.SeedDb();
        }

        private void TruncateQuotesTable()
        {
            var quotes = this.dbService.DbContext.Quotes.ToList();
            this.dbService.DbContext.Quotes.RemoveRange(quotes);

            this.dbService.DbContext.SaveChanges();
        }

        private void TruncateUsersTable()
        {
            var categories = this.dbService.DbContext.Users.ToList();
            this.dbService.DbContext.Users.RemoveRange(categories);

            this.dbService.DbContext.SaveChanges();
        }

        private void TruncateForumsTable()
        {
            var forums = this.dbService.DbContext.Forums.ToList();
            this.dbService.DbContext.Forums.RemoveRange(forums);

            this.dbService.DbContext.SaveChanges();
        }

        private void TruncatePostsTable()
        {
            var posts = this.dbService.DbContext.Posts.ToList();
            this.dbService.DbContext.Posts.RemoveRange(posts);

            this.dbService.DbContext.SaveChanges();
        }

        private void SeedDb()
        {
            this.TruncateForumsTable();
            this.TruncatePostsTable();
            this.TruncateQuotesTable();
            this.TruncateUsersTable();

            var post = new Models.Post { Id = TestsConstants.TestId };
            this.dbService.DbContext.Posts.Add(post);
            this.dbService.DbContext.SaveChanges();

            var reply = new Models.Reply { Id = TestsConstants.TestId2, Post = post, PostId = post.Id };
            this.dbService.DbContext.Replies.Add(reply);
            this.dbService.DbContext.SaveChanges();

            var author = new ForumUser { Id = TestsConstants.TestId, UserName = TestsConstants.TestUsername1 };
            var reciever = new ForumUser { Id = TestsConstants.TestId1, UserName = TestsConstants.TestUsername2 };

            this.dbService.DbContext.Users.Add(author);
            this.dbService.DbContext.Users.Add(reciever);
            this.dbService.DbContext.SaveChanges();

            var firstQuote = new Models.Quote { Id = TestsConstants.TestId1, Author = author, AuthorId = author.Id, Reply = reply, ReplyId = reply.Id };
            this.dbService.DbContext.Quotes.Add(firstQuote);
            this.dbService.DbContext.SaveChanges();

            for (int i = 0; i < 5; i++)
            {
                var quote = new Models.Quote { Id = Guid.NewGuid().ToString(), Author = author, AuthorId = author.Id, Reply = reply, ReplyId = reply.Id };

                this.dbService.DbContext.Quotes.Add(quote);
                this.dbService.DbContext.SaveChanges();
            }
        }

        [Fact]
        public void DeleteUserQuotes_returns_correct_result_when_correct()
        {
            var user = this.dbService.DbContext.Users.FirstOrDefault(u => u.Id == TestsConstants.TestId);

            var expectedResult = 6;

            var actualResult = this.quoteService.DeleteUserQuotes(user);

            Assert.Equal(expectedResult, actualResult);

            this.SeedDb();
        }

        [Fact]
        public void GetQuote_returns_entity_result_when_correct()
        {
            var expectedResult = this.dbService.DbContext.Quotes.FirstOrDefault(q => q.Id == TestsConstants.TestId1);

            var actualResult = this.quoteService.GetQuote(TestsConstants.TestId1, new ModelStateDictionary());

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetQuote_returns_null_result_when_incorrect()
        {
            var actualResult = this.quoteService.GetQuote(Guid.NewGuid().ToString(), new ModelStateDictionary());

            Assert.Null(actualResult);
        }

        [Fact]
        public void AddQuote_returns_one_result_when_correct()
        {
            var author = this.dbService.DbContext.Users.FirstOrDefault(u => u.Id == TestsConstants.TestId);

            var model = new QuoteInputModel
            {
                Quote = TestsConstants.ParsedValidPostDescription,
                Description = TestsConstants.ValidPostDescription,
                RecieverId = TestsConstants.TestId1,
                ReplyId = TestsConstants.TestId2
            };

            var expectedResult = 1;

            var actualResult = this.quoteService.AddQuote(model, author, TestsConstants.TestUsername2);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetQuotesByForum_returns_correct_list_when_correct()
        {
            var expectedResult = 6;

            var actualResult = this.quoteService.GetQuotesByPost(TestsConstants.TestId);

            Assert.Equal(expectedResult, actualResult.Count());
        }
    }
}