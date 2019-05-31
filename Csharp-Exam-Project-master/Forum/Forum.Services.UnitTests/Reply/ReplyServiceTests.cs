using AutoMapper;
using Forum.Models;
using Forum.Services.Common;
using Forum.Services.Interfaces.Db;
using Forum.Services.Interfaces.Reply;
using Forum.Services.UnitTests.Base;
using Forum.ViewModels.Reply;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Forum.Services.UnitTests.Reply
{
    public class ReplyServiceTests : IClassFixture<BaseUnitTest>
    {
        private readonly IDbService dbService;

        private readonly IMapper mapper;

        private readonly IReplyService replyService;

        public ReplyServiceTests(BaseUnitTest fixture)
        {
            this.dbService = fixture.Provider.GetService(typeof(IDbService)) as IDbService;

            this.mapper = fixture.Provider.GetService(typeof(IMapper)) as IMapper;

            this.replyService = fixture.Provider.GetService(typeof(IReplyService)) as IReplyService;

            this.SeedDb();
        }

        private void TruncateRepliesTable()
        {
            var replies = this.dbService.DbContext.Replies.ToList();
            this.dbService.DbContext.Replies.RemoveRange(replies);

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
            this.TruncateUsersTable();
            this.TruncateRepliesTable();

            var user = new ForumUser { Id = TestsConstants.TestId, UserName = TestsConstants.TestUsername1 };
            this.dbService.DbContext.Users.Add(user);
            this.dbService.DbContext.SaveChanges();

            var post = new Models.Post { Author = user, Description = TestsConstants.ValidPostDescription, AuthorId = user.Id, Id = TestsConstants.TestId3 };
            this.dbService.DbContext.Posts.Add(post);
            this.dbService.DbContext.SaveChanges();

            var firstReply = new Models.Reply { Id = TestsConstants.TestId, Author = user, AuthorId = user.Id, Post = post, PostId = post.Id };
            this.dbService.DbContext.Replies.Add(firstReply);
            this.dbService.DbContext.SaveChanges();

            for (int i = 0; i < 5; i++)
            {
                var reply = new Models.Reply { Id = Guid.NewGuid().ToString(), Author = user, AuthorId = user.Id, Post = post, PostId = post.Id };

                this.dbService.DbContext.Replies.Add(reply);
                this.dbService.DbContext.SaveChanges();
            }
        }

        [Fact]
        public void DeleteUserReplies_returns_correct_result_when_correct()
        {
            var expectedResult = 6;

            var actualResult = this.replyService.DeleteUserReplies(TestsConstants.TestUsername1);

            Assert.Equal(expectedResult, actualResult);

            this.SeedDb();
        }

        [Fact]
        public void GetReply_returns_entity_result_when_correct()
        {
            var reply = this.dbService.DbContext.Replies.FirstOrDefault(r => r.Id == TestsConstants.TestId);

            var expectedResult = reply;

            var actualResult = this.replyService.GetReply(reply.Id, new ModelStateDictionary());

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetReply_returns_null_result_when_incorrect()
        {
            var actualResult = this.replyService.GetReply(Guid.NewGuid().ToString(), new ModelStateDictionary());

            Assert.Null(actualResult);
        }

        [Fact]
        public void AddReply_returns_entity_result_when_correct()
        {
            var user = this.dbService.DbContext.Users.FirstOrDefault(u => u.Id == TestsConstants.TestId);
            
            var model = new ReplyInputModel { Description = TestsConstants.ValidPostDescription, Author = user, PostId = TestsConstants.TestId3 };

            var expectedResult = 1;

            var actualResult = this.replyService.AddReply(model, user).GetAwaiter().GetResult();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GePostRepliesIds_returns_correct_list_when_correct()
        {
            var post = this.dbService.DbContext.Posts.FirstOrDefault(p => p.Id == TestsConstants.TestId3);

            var expectedResult = 6;

            var actualResult = this.replyService.GetPostRepliesIds(post.Id).Count();

            Assert.Equal(expectedResult, actualResult);
        }
    }
}