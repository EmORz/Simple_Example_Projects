using AutoMapper;
using Forum.Models;
using Forum.Models.Enums;
using Forum.Services.Common;
using Forum.Services.Interfaces.Db;
using Forum.Services.Interfaces.Post;
using Forum.Services.UnitTests.Base;
using Forum.ViewModels.Post;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace Forum.Services.UnitTests.Post
{
    public class PostServiceTests : IClassFixture<BaseUnitTest>
    {
        private readonly IDbService dbService;

        private readonly IPostService postService;

        private UserManager<ForumUser> userManager;

        public PostServiceTests(BaseUnitTest fixture)
        {
            this.dbService = fixture.Provider.GetService(typeof(IDbService)) as IDbService;

            this.postService = fixture.Provider.GetService(typeof(IPostService)) as IPostService;

            this.userManager = fixture.Provider.GetService(typeof(UserManager<ForumUser>)) as UserManager<ForumUser>;

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

        private void SeedDb()
        {
            this.TruncateCategoriesTable();
            this.TruncateForumsTable();
            this.TruncatePostsTable();
            this.TruncateUsersTable();
            this.TruncateRolesTable();
            this.TruncateUserRolesTable();

            var ownerRole = new IdentityRole { Id = TestsConstants.TestId1, ConcurrencyStamp = "test", NormalizedName = Common.Role.Owner.ToUpper(), Name = Common.Role.Owner };
            var adminRole = new IdentityRole { Id = TestsConstants.TestId2, ConcurrencyStamp = "test", NormalizedName = Common.Role.Administrator.ToUpper(), Name = Common.Role.Administrator };
            var userRole = new IdentityRole { Id = TestsConstants.TestId3, ConcurrencyStamp = "test", NormalizedName = Common.Role.User.ToUpper(), Name = Common.Role.User };

            this.dbService.DbContext.Roles.Add(ownerRole);
            this.dbService.DbContext.Roles.Add(adminRole);
            this.dbService.DbContext.Roles.Add(userRole);
            this.dbService.DbContext.SaveChanges();

            var user = new ForumUser { Id = TestsConstants.TestId1, SecurityStamp = "test", UserName = TestsConstants.TestUsername1 };
            var secondUser = new ForumUser { Id = TestsConstants.TestId3, SecurityStamp = "test", UserName = TestsConstants.TestUsername2 };
            this.dbService.DbContext.Users.Add(user);
            this.dbService.DbContext.Users.Add(secondUser);
            this.dbService.DbContext.SaveChanges();

            var adminCategory = new Models.Category { Id = Guid.NewGuid().ToString(), Type = CategoryType.AdminOnly };
            var publicCategory = new Models.Category { Id = Guid.NewGuid().ToString(), Type = CategoryType.Public };
            this.dbService.DbContext.Categories.Add(adminCategory);
            this.dbService.DbContext.Categories.Add(publicCategory);
            this.dbService.DbContext.SaveChanges();

            var publicForum = new SubForum { Id = Guid.NewGuid().ToString(), Category = adminCategory, CategoryId = adminCategory.Id };
            var adminForum = new SubForum { Id = Guid.NewGuid().ToString(), Category = publicCategory, CategoryId = publicCategory.Id };
            this.dbService.DbContext.Forums.Add(publicForum);
            this.dbService.DbContext.Forums.Add(adminForum);
            this.dbService.DbContext.SaveChanges();

            var userOwner = this.userManager.AddToRoleAsync(user, Common.Role.Owner).GetAwaiter().GetResult();
            var secondUserOwner = this.userManager.AddToRoleAsync(user, Common.Role.User).GetAwaiter().GetResult();

            var post = new Models.Post { Name = TestsConstants.ValidPostName, Id = TestsConstants.TestId, Forum = adminForum, ForumId = adminForum.Id };
            var secondPost = new Models.Post { Name = TestsConstants.ValidPostName, Id = TestsConstants.TestId2, Forum = publicForum, ForumId = publicForum.Id, Views = 50 };
            this.dbService.DbContext.Posts.Add(post);
            this.dbService.DbContext.Posts.Add(secondPost);
            this.dbService.DbContext.SaveChanges();
        }

        [Fact]
        public void DoesPostExist_returns_true_when_correct()
        {
            var actualResult = this.postService.DoesPostExist(TestsConstants.TestId);
            Assert.True(actualResult == true);
        }

        [Fact]
        public void DoesPostExist_returns_false_when_incorrect()
        {
            var actualResult = this.postService.DoesPostExist(TestsConstants.TestId1);

            Assert.True(actualResult == false);
        }

        [Fact]
        public void GetTotalPostsCount_returns_correct_result_when_correct()
        {
            var expectedResult = 2;

            var actualResult = this.postService.GetTotalPostsCount();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ViewPost_increments_post_views_when_correct()
        {
            var expectedResult = 1;

            var actualResult = this.postService.ViewPost(TestsConstants.TestId);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void AddPost_returns_one_when_correct()
        {
            var user = new ForumUser { Id = Guid.NewGuid().ToString(), UserName = TestsConstants.TestUsername3 };
            this.dbService.DbContext.Users.Add(user);
            this.dbService.DbContext.SaveChanges();

            var forum = new SubForum { Id = Guid.NewGuid().ToString(), Posts = new List<Models.Post>() };
            this.dbService.DbContext.Forums.Add(forum);
            this.dbService.DbContext.SaveChanges();

            var model = new PostInputModel { ForumId = forum.Id, Description = TestsConstants.ValidPostDescription, ForumName = forum.Name, Name = TestsConstants.ValidPostName };

            var expectedResult = 1;

            var actualResult = this.postService.AddPost(model, user, forum.Id).GetAwaiter().GetResult();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void ParseDescription_returns_parsed_tags_when_correct()
        {
            var exampleDescription = TestsConstants.ValidPostDescription;

            var expectedResult = TestsConstants.ParsedValidPostDescription;

            var actualResult = this.postService.ParseDescription(exampleDescription);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetPost_returns_entity_correct()
        {
            var claims = new List<Claim>
            {
                new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", Common.Role.Owner)
            };

            var identity = new ClaimsIdentity(claims, "Test");

            var principal = new ClaimsPrincipal(identity);

            var expectedResult = TestsConstants.TestId;

            var actualResult = this.postService.GetPost(TestsConstants.TestId, 0, principal, new ModelStateDictionary());

            Assert.Equal(expectedResult, actualResult.Id);
        }

        [Fact]
        public void GetPost_returns_null()
        {
            var claims = new List<Claim>
            {
                new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", Common.Role.User)
            };

            var identity = new ClaimsIdentity(claims, "Test");

            var principal = new ClaimsPrincipal(identity);

            var actualResult = this.postService.GetPost(Guid.NewGuid().ToString(), 0, principal, new ModelStateDictionary());

            Assert.Null(actualResult);
        }

        [Fact]
        public void GetLatestPosts_returns_correct_list_when_owner_correct()
        {
            var expectedResult = 2;

            var claims = new List<Claim>
            {
                new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", Common.Role.Owner)
            };

            var identity = new ClaimsIdentity(claims, "Test");

            var principal = new ClaimsPrincipal(identity);

            var actualResult = this.postService.GetLatestPosts(principal).Count();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetLatestPosts_returns_correct_list_when_user_correct()
        {
            var expectedResult = 1;

            var claims = new List<Claim>
            {
                new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", Common.Role.User)
            };

            var identity = new ClaimsIdentity(claims, "Test");

            var principal = new ClaimsPrincipal(identity);

            var actualResult = this.postService.GetLatestPosts(principal).Count();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetPopularPosts_returns_correct_list_when_owner_correct()
        {
            var expectedResult = new List<int> { 50, 0 };

            var claims = new List<Claim>
            {
                new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", Common.Role.Owner)
            };

            var identity = new ClaimsIdentity(claims, "Test");

            var principal = new ClaimsPrincipal(identity);

            var actualResult = this.postService.GetPopularPosts(principal).Select(p => p.Views).ToList();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetPopularPosts_returns_correct_list_when_user_correct()
        {
            var expectedResult = new List<int> { 0 };

            var claims = new List<Claim>
            {
                new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", Common.Role.User)
            };

            var identity = new ClaimsIdentity(claims, "Test");

            var principal = new ClaimsPrincipal(identity);

            var actualResult = this.postService.GetPopularPosts(principal).Select(p => p.Views).ToList();

            Assert.Equal(expectedResult, actualResult);
        }

        //[Fact]
        //public void Edit_returns_one_when_correct()
        //{
        //    var model = new EditPostInputModel { Description = TestsConstants.ValidPostDescription, Id = TestsConstants.TestId };

        //    var expectedResult = 1;

        //    var actualResult = this.postService.Edit(model, new ModelStateDictionary());

        //    Assert.Equal(expectedResult, actualResult);
        //}

        [Fact]
        public void GetEditPostModel_returns_one_when_correct()
        {
            var expectedResult = TestsConstants.TestId;

            var claims = new List<Claim>
            {
                new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", Common.Role.Owner)
            };

            var identity = new ClaimsIdentity(claims, "Test");

            var principal = new ClaimsPrincipal(identity);

            var actualResult = this.postService.GetEditPostModel(TestsConstants.TestId, principal, new ModelStateDictionary());

            Assert.Equal(expectedResult, actualResult.Id);
        }
    }
}