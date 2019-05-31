using AutoMapper;
using Forum.Models;
using Forum.Services.Common;
using Forum.Services.Interfaces.Account;
using Forum.Services.Interfaces.Db;
using Forum.Services.UnitTests.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Forum.Services.UnitTests.Account
{
    public class AccountServiceTests : IClassFixture<BaseUnitTest>
    {
        private readonly IMapper mapper;

        private readonly IAccountService accountService;

        private readonly IDbService dbService;

        public AccountServiceTests(BaseUnitTest fixture)
        {
            this.mapper = fixture.Provider.GetService(typeof(IMapper)) as IMapper;
            this.accountService = fixture.Provider.GetService(typeof(IAccountService)) as IAccountService;
            this.dbService = fixture.Provider.GetService(typeof(IDbService)) as IDbService;

            this.SeedDb();
        }

        private void SeedDb()
        {
            this.TruncateRolesTable();
            this.TruncateUsersTable();
            this.TruncateUserRolesTable();

            var user = new ForumUser { Id = TestsConstants.TestId, UserName = TestsConstants.TestUsername1, RegisteredOn = DateTime.UtcNow.AddDays(1) };
            var secondUser = new ForumUser { Id = TestsConstants.TestId2, Email = TestsConstants.TestEmail, UserName = TestsConstants.TestUsername2, RegisteredOn = DateTime.UtcNow.AddDays(2) };
            var thirdUser = new ForumUser { Id = TestsConstants.TestId3, UserName = TestsConstants.TestUsername3, RegisteredOn = DateTime.UtcNow.AddDays(3) };

            this.dbService.DbContext.Users.Add(user);
            this.dbService.DbContext.Users.Add(secondUser);
            this.dbService.DbContext.Users.Add(thirdUser);
            this.dbService.DbContext.SaveChanges();

            var ownerRole = new IdentityRole { Id = Guid.NewGuid().ToString(), Name = Common.Role.Owner };
            var userRole = new IdentityRole { Id = Guid.NewGuid().ToString(), Name = Common.Role.User };

            this.dbService.DbContext.Roles.Add(ownerRole);
            this.dbService.DbContext.Roles.Add(userRole);
            this.dbService.DbContext.SaveChanges();

            var testSecondUserRole = new IdentityUserRole<string> { RoleId = userRole.Id, UserId = secondUser.Id };
            var testThirdUserRole = new IdentityUserRole<string> { RoleId = userRole.Id, UserId = thirdUser.Id };

            this.dbService.DbContext.UserRoles.Add(testSecondUserRole);
            this.dbService.DbContext.UserRoles.Add(testThirdUserRole);
            this.dbService.DbContext.SaveChanges();

            var roleId = ownerRole.Id;
            var userId = user.Id;

            var newUserRole = new IdentityUserRole<string> { RoleId = roleId, UserId = userId };
            this.dbService.DbContext.UserRoles.Add(newUserRole);
            this.dbService.DbContext.SaveChanges();
        }

        private void TruncateUsersTable()
        {
            var users = this.dbService.DbContext.Users.ToList();

            this.dbService.DbContext.Users.RemoveRange(users);
            this.dbService.DbContext.SaveChanges();
        }

        private void TruncateRolesTable()
        {
            var roles = this.dbService.DbContext.Roles.ToList();

            this.dbService.DbContext.Roles.RemoveRange(roles);
            this.dbService.DbContext.SaveChanges();
        }

        private void TruncateUserRolesTable()
        {
            var userRoles = this.dbService.DbContext.UserRoles.ToList();

            this.dbService.DbContext.UserRoles.RemoveRange(userRoles);
            this.dbService.DbContext.SaveChanges();
        }

        [Fact]
        public void UsernameExists_returns_true_when_correct()
        {
            Assert.True(this.accountService.UsernameExists(TestsConstants.TestUsername1) == true);
        }

        [Fact]
        public void UsernameExists_returns_false_when_incorrect()
        {
            Assert.True(this.accountService.UsernameExists(TestsConstants.TestUsername4) == false);
        }

        [Fact]
        public void UserExists_returns_true_when_correct()
        {
            Assert.True(this.accountService.UserExists(TestsConstants.TestUsername1) == true);
        }

        [Fact]
        public void UserExists_returns_false_when_incorrect()
        {
            Assert.True(this.accountService.UserExists(TestsConstants.TestUsername4) == false);
        }

        [Fact]
        public void GetUserById_returns_user_when_correct()
        {
            var expectedResult = new ForumUser { Id = TestsConstants.TestId, UserName = TestsConstants.TestUsername1 };

            Assert.Equal(expectedResult.UserName, this.accountService.GetUserById(expectedResult.Id, new ModelStateDictionary()).UserName);
        }

        [Fact]
        public void GetUserById_returns_null_when_incorrect()
        {
            Assert.Null(this.accountService.GetUserById(Guid.NewGuid().ToString(), new ModelStateDictionary()));
        }

        [Fact]
        public void GetUserByName_returns_user_when_correct()
        {
            var actualResult = this.accountService.GetUserByName(TestsConstants.TestUsername1, new ModelStateDictionary()).UserName;

            var expectedResult = TestsConstants.TestUsername1;

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetUserByName_returns_null_when_incorrect()
        {
            var actualResult = this.accountService.GetUserByName(TestsConstants.TestUsername4, new ModelStateDictionary());

            Assert.Null(actualResult);
        }

        [Fact]
        public void GetUsernames_returns_correct_list_when_correct()
        {
            var expectedList = new List<string> { TestsConstants.TestUsername1, TestsConstants.TestUsername2, TestsConstants.TestUsername3 }.OrderBy(n => n).ToList();
            var actualList = this.accountService.GetUsernames().OrderBy(n => n).ToList();

            Assert.Equal(expectedList, actualList);
        }

        [Fact]
        public void GetUsernames_returns_empty_list_when_correct()
        {
            this.TruncateUsersTable();
            var expectedResult = new List<string>();
            var actualResult = this.accountService.GetUsernames();

            Assert.Equal(expectedResult, actualResult);

            this.SeedDb();
        }

        [Fact]
        public void GetUsers_returns_correct_list_when_correct()
        {
            var actualList = this.accountService.GetUsers().OrderBy(u => u.UserName).Select(u => u.UserName);

            var expectedList = new List<string>
            {
                TestsConstants.TestUsername1,
                TestsConstants.TestUsername2,
                TestsConstants.TestUsername3
            }.OrderBy(u => u);

            Assert.Equal(expectedList, actualList);
        }

        [Fact]
        public void GetUsers_returns_empty_list_when_correct()
        {
            this.TruncateUsersTable();

            var expectedResult = new List<ForumUser>();

            var actualResult = this.accountService.GetUsers();

            Assert.Equal(expectedResult, actualResult);

            this.SeedDb();
        }

        [Fact]
        public void GetNewestUser_returns_username_when_correct()
        {
            var expectedResult = TestsConstants.TestUsername3;

            var actualResult = this.accountService.GetNewestUser();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetNewestUser_returns_empty_string_when_correct()
        {
            this.TruncateUsersTable();
            this.TruncateRolesTable();
            this.TruncateUserRolesTable();

            var expectedResult = string.Empty;

            var actualResult = this.accountService.GetNewestUser();

            Assert.Equal(expectedResult, actualResult);

            this.SeedDb();
        }

        [Fact]
        public void EmailExists_returns_true_string_when_correct()
        {
            var actualResult = this.accountService.EmailExists(TestsConstants.TestEmail);

            Assert.True(actualResult == true);
        }

        [Fact]
        public void GetUsernamesWithourOwner_returns_correct_list_with_entities()
        {
            this.TruncateUsersTable();
            this.TruncateRolesTable();
            this.TruncateUserRolesTable();

            this.SeedDb();

            var expectedResult = new List<string> { TestsConstants.TestUsername2, TestsConstants.TestUsername3 }.OrderBy(n => n);

            var actualResult = this.accountService.GetUsernamesWithoutOwner().OrderBy(n => n);

            Assert.Equal(expectedResult, actualResult);
        }
    }
}