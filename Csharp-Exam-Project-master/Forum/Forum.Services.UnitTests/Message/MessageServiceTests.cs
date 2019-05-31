using AutoMapper;
using Forum.Models;
using Forum.Services.Common;
using Forum.Services.Interfaces.Db;
using Forum.Services.Interfaces.Message;
using Forum.Services.UnitTests.Base;
using Forum.ViewModels.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Forum.Services.UnitTests.Message
{
    public class MessageServiceTests : IClassFixture<BaseUnitTest>
    {
        private readonly IDbService dbService;

        private readonly IMapper mapper;

        private readonly IMessageService messageService;

        public MessageServiceTests(BaseUnitTest fixture)
        {
            this.dbService = fixture.Provider.GetService(typeof(IDbService)) as IDbService;

            this.mapper = fixture.Provider.GetService(typeof(IMapper)) as IMapper;

            this.messageService = fixture.Provider.GetService(typeof(IMessageService)) as IMessageService;

            this.SeedDb();
        }

        private void TruncateCategoriesTable()
        {
            var categories = this.dbService.DbContext.Categories.ToList();
            this.dbService.DbContext.Categories.RemoveRange(categories);

            this.dbService.DbContext.SaveChanges();
        }

        private void TruncateUsersTable()
        {
            var categories = this.dbService.DbContext.Users.ToList();
            this.dbService.DbContext.Users.RemoveRange(categories);

            this.dbService.DbContext.SaveChanges();
        }

        private void TruncateMessagesTable()
        {
            var messages = this.dbService.DbContext.Messages.ToList();
            this.dbService.DbContext.Messages.RemoveRange(messages);

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
            this.TruncateCategoriesTable();
            this.TruncateMessagesTable();
            this.TruncatePostsTable();
            this.TruncateUsersTable();

            ForumUser user, secondUser;

            SeedUsers(out user, out secondUser);

            for (int i = 0; i < 5; i++)
            {
                var message = new Models.Message
                {
                    Description = TestsConstants.ValidTestMessageDescription,
                    Id = Guid.NewGuid().ToString(),
                    Author = user,
                    AuthorId = user.Id,
                    Reciever = secondUser,
                    RecieverId = secondUser.Id,
                    CreatedOn = DateTime.UtcNow.AddDays(i)
                };
                var secondMessage = new Models.Message
                {
                    Description = TestsConstants.ValidTestMessageDescription,
                    Id = Guid.NewGuid().ToString(),
                    Author = secondUser,
                    AuthorId = secondUser.Id,
                    Reciever = user,
                    RecieverId = user.Id,
                    CreatedOn = DateTime.UtcNow.AddDays(i)
                };

                this.dbService.DbContext.Messages.Add(message);
                this.dbService.DbContext.Messages.Add(secondMessage);
                this.dbService.DbContext.SaveChanges();
            }
        }

        private void SeedUsers(out ForumUser user, out ForumUser secondUser)
        {
            user = new ForumUser { Id = TestsConstants.TestId, SecurityStamp = "test", UserName = TestsConstants.TestUsername1, RegisteredOn = DateTime.UtcNow.AddDays(1) };
            secondUser = new ForumUser { Id = TestsConstants.TestId2, SecurityStamp = "test", Email = TestsConstants.TestEmail, UserName = TestsConstants.TestUsername2, RegisteredOn = DateTime.UtcNow.AddDays(2) };
            var thirdUser = new ForumUser { Id = TestsConstants.TestId3, SecurityStamp = "test", UserName = TestsConstants.TestUsername3, RegisteredOn = DateTime.UtcNow.AddDays(3) };

            this.dbService.DbContext.Users.Add(user);
            this.dbService.DbContext.Users.Add(secondUser);
            this.dbService.DbContext.Users.Add(thirdUser);
            this.dbService.DbContext.SaveChanges();
        }

        [Fact]
        public void RemoveUserMessages_returns_zero_when_correct()
        {
            var expectedResult = 10;
            var actualResult = this.messageService.RemoveUserMessages(TestsConstants.TestUsername1);

            Assert.Equal(expectedResult, actualResult);
            this.SeedDb();
        }

        [Fact]
        public void SendMessage_returns_one_when_correct()
        {
            var message = new SendMessageInputModel
            {
                Description = TestsConstants.ValidTestMessageDescription,
                ShowAll = false,
                RecieverId = TestsConstants.TestId1,
                RecieverName = TestsConstants.TestUsername2
            };

            var expectedResult = 1;
            var actualResult = this.messageService.SendMessage(message, TestsConstants.TestId);

            Assert.Equal(expectedResult, actualResult);

            this.SeedDb();
        }

        [Fact]
        public void SendMessage_returns_zero_when_incorrect()
        {
            var message = new SendMessageInputModel
            {
                Description = TestsConstants.InvalidTestMessageDescription,
                ShowAll = false,
                RecieverId = TestsConstants.TestId1,
                RecieverName = TestsConstants.TestUsername2.ToUpper()
            };

            var expectedResult = 0;
            var actualResult = this.messageService.SendMessage(message, TestsConstants.TestId);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetRecentConversations_returns_list_when_correct()
        {
            var expectedResult = new List<string> { TestsConstants.TestUsername2 }.ToList();
            var actualResult = this.messageService.GetRecentConversations(TestsConstants.TestUsername1).OrderBy(n => n).ToList();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetConversationMessages_showall_false_returns_list_when_correct()
        {
            var expectedResult = 5;
            var actualResult = this.messageService.GetConversationMessages(TestsConstants.TestUsername1, TestsConstants.TestUsername2, false).ToList();

            Assert.Equal(expectedResult, actualResult.Count());
        }

        [Fact]
        public void GetConversationMessages_showall_true_returns_list_when_correct()
        {
            var expectedResult = 10;

            var actualResult = this.messageService.GetConversationMessages(TestsConstants.TestUsername1, TestsConstants.TestUsername2, true).ToList();

            Assert.Equal(expectedResult, actualResult.Count());
        }

        [Fact]
        public void GetUnreadMessages_returns_correct_count_when_correct()
        {
            var expectedResult = 5;

            var actualResult = this.messageService.GetUnreadMessages(TestsConstants.TestUsername1).Sum(um => um.MessagesCount);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetLatestMessages_returns_empty_list_when_icorrect()
        {
            this.TruncateMessagesTable();

            var expectedResult = new List<ChatMessageViewModel>();

            var actualResult = this.messageService.GetLatestMessages(TestsConstants.InvalidDateTime, TestsConstants.TestUsername1, TestsConstants.TestUsername1);

            Assert.Equal(expectedResult, actualResult);
        }
    }
}