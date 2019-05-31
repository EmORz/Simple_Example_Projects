using AutoMapper;
using Forum.Models;
using Forum.Models.Enums;
using Forum.Services.Common;
using Forum.Services.Interfaces.Db;
using Forum.Services.Interfaces.Forum;
using Forum.Services.UnitTests.Base;
using Forum.ViewModels.Forum;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace Forum.Services.UnitTests.Forum
{
    public class ForumServiceTests : IClassFixture<BaseUnitTest>
    {
        private readonly SignInManager<ForumUser> signInManager;

        private readonly UserManager<ForumUser> userManager;

        private readonly IMapper mapper;

        private readonly IForumService forumService;

        private readonly IDbService dbService;

        public ForumServiceTests(BaseUnitTest fixture)
        {
            this.mapper = fixture.Provider.GetService(typeof(IMapper)) as IMapper;

            this.dbService = fixture.Provider.GetService(typeof(IDbService)) as IDbService;

            this.forumService = fixture.Provider.GetService(typeof(IForumService)) as IForumService;

            this.signInManager = fixture.Provider.GetService(typeof(SignInManager<ForumUser>)) as SignInManager<ForumUser>;

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

        private void SeedDb()
        {
            this.TruncateCategoriesTable();
            this.TruncateForumsTable();
            this.TruncatePostsTable();
            this.TruncateUsersTable();
            this.TruncateRolesTable();

            var ownerRole = new IdentityRole { Id = TestsConstants.TestId1, ConcurrencyStamp = "test", NormalizedName = Common.Role.Owner.ToUpper(), Name = Common.Role.Owner };
            var adminRole = new IdentityRole { Id = TestsConstants.TestId2, ConcurrencyStamp = "test", NormalizedName = Common.Role.Administrator.ToUpper(), Name = Common.Role.Administrator };
            var userRole = new IdentityRole { Id = TestsConstants.TestId3, ConcurrencyStamp = "test", NormalizedName = Common.Role.User.ToUpper(), Name = Common.Role.User };

            this.dbService.DbContext.Roles.Add(ownerRole);
            this.dbService.DbContext.Roles.Add(adminRole);
            this.dbService.DbContext.Roles.Add(userRole);
            this.dbService.DbContext.SaveChanges();

            ForumUser user, secondUser;

            SeedUsers(out user, out secondUser);

            this.userManager.AddToRoleAsync(user, Common.Role.Owner).GetAwaiter().GetResult();
            this.userManager.AddToRoleAsync(secondUser, Common.Role.User).GetAwaiter().GetResult();

            Models.Category category = SeedCategories(user, secondUser);

            var forum = new SubForum { Category = category, CategoryId = category.Id, Id = TestsConstants.TestId, Name = TestsConstants.ValidForumName };
            this.dbService.DbContext.Forums.Add(forum);
            this.dbService.DbContext.SaveChanges();

            var post = new Models.Post { Id = TestsConstants.TestId, Name = TestsConstants.ValidPostName, Forum = forum, ForumId = forum.Id };
            this.dbService.DbContext.Posts.Add(post);
            this.dbService.DbContext.SaveChanges();
        }

        private Models.Category SeedCategories(ForumUser user, ForumUser secondUser)
        {
            var category = new Models.Category { Id = TestsConstants.TestId, Name = TestsConstants.ValidCategoryName, User = user, UserId = user.Id, Type = CategoryType.AdminOnly };
            var secondCategory = new Models.Category { Id = TestsConstants.TestId1, Name = TestsConstants.ValidCategoryName1, User = secondUser, UserId = secondUser.Id, Type = CategoryType.Public };

            this.dbService.DbContext.Categories.Add(category);
            this.dbService.DbContext.Categories.Add(secondCategory);
            this.dbService.DbContext.SaveChanges();
            return category;
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
        public void GetForum_returns_entity_when_correct()
        {
            var expectedResult = TestsConstants.TestId;

            var actualResult = this.forumService.GetForum(TestsConstants.TestId, new ModelStateDictionary()).Id;

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetForum_returns_null_when_incorrect()
        {
            var actualResult = this.forumService.GetForum(Guid.NewGuid().ToString(), new ModelStateDictionary());

            Assert.Null(actualResult);
        }

        [Fact]
        public void GetPostsByForum_returns_empty_list_when_correct()
        {
            this.TruncatePostsTable();

            var expectedList = new List<Models.Post>();

            var actualList = this.forumService.GetPostsByForum(TestsConstants.TestId, 0);

            Assert.Equal(expectedList, actualList);

            this.SeedDb();
        }

        [Fact]
        public void GetPostsByForum_returns_entities_when_correct()
        {
            var expectedList = new List<string> { TestsConstants.TestId };

            var actualList = this.forumService.GetPostsByForum(TestsConstants.TestId, 0).Select(p => p.Id);

            Assert.Equal(expectedList, actualList);
        }

        [Fact]
        public void AddForum_returns_one_when_correct()
        {
            var category = new Models.Category { Id = Guid.NewGuid().ToString(), Name = TestsConstants.ValidCategoryName2 };
            this.dbService.DbContext.Categories.Add(category);
            this.dbService.DbContext.SaveChanges();

            var model = new ForumFormInputModel();
            model.ForumModel.Name = TestsConstants.ValidForumName;
            model.ForumModel.Category = category.Name;

            var expectedResult = 1;

            var actualResult = this.forumService.AddForum(model, category.Id);

            Assert.Equal(expectedResult, actualResult);

            this.SeedDb();
        }

        [Fact]
        public void Edit_returns_one_when_correct()
        {
            var forum = this.dbService.DbContext.Forums.First();

            var model = this.mapper.Map<EditForumInputModel>(forum);
            model.Category = TestsConstants.TestId;

            var expectedResult = 1;

            var actualResult = this.forumService.Edit(model, forum.Id);

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void Delete_returns_two_when_correct()
        {
            var forum = this.dbService.DbContext.Forums.First();

            //returns 2 because it deletes all the forum's posts and then the forum itself
            var expectedResult = 2;

            var actualResult = this.forumService.Delete(forum);

            Assert.Equal(expectedResult, actualResult);

            this.SeedDb();
        }

        [Fact]
        public void ForumExists_returns_true_when_correct()
        {
            var actualResult = this.forumService.ForumExists(TestsConstants.ValidForumName);

            Assert.True(actualResult == true);
        }

        [Fact]
        public void ForumExists_returns_false_when_correct()
        {
            var actualResult = this.forumService.ForumExists(TestsConstants.ValidForumName1);

            Assert.True(actualResult == false);
        }

        [Fact]
        public void GetForumPostsIds_returns_ids_when_correct()
        {
            var expectedList = new List<string> { TestsConstants.TestId };

            var actualList = this.forumService.GetForumPostsIds(TestsConstants.TestId);

            Assert.Equal(expectedList, actualList);
        }

        [Fact]
        public void GetAllForums_returns_entities_when_Owner_correct()
        {
            var expectedResult = 1;

            var claims = new List<Claim>
            {
                new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", Common.Role.Owner)
            };

            var identity = new ClaimsIdentity(claims, "Test");

            var actualResult = this.forumService.GetAllForums(new ClaimsPrincipal(identity)).Count();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetAllForums_returns_entities_when_User_correct()
        {
            var expectedResult = 0;

            var claims = new List<Claim>
            {
                new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/role", Common.Role.User)
            };

            var identity = new ClaimsIdentity(claims, "Test");

            var actualResult = this.forumService.GetAllForums(new ClaimsPrincipal(identity)).Count();

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetAllForumsIds_returns_entities_when_Owner_correct()
        {
            var expectedResult = new List<string> { TestsConstants.TestId }.OrderBy(id => id);

            var user = this.dbService.DbContext.Users.FirstOrDefault(u => u.Id == TestsConstants.TestId);

            var principal = this.signInManager.CreateUserPrincipalAsync(user).GetAwaiter().GetResult();

            var actualList = this.forumService.GetAllForumsIds(principal, new ModelStateDictionary(), TestsConstants.TestId).OrderBy(id => id);

            Assert.Equal(expectedResult, actualList);
        }

        [Fact]
        public void GetAllForumsIds_returns_null_when_incorrect()
        {
            var user = this.dbService.DbContext.Users.FirstOrDefault(u => u.Id == TestsConstants.TestId2);

            var principal = this.signInManager.CreateUserPrincipalAsync(user).GetAwaiter().GetResult();

            var actualList = this.forumService.GetAllForumsIds(principal, new ModelStateDictionary(), Guid.NewGuid().ToString());

            Assert.Null(actualList);
        }

        [Fact]
        public void GetMappedForumModel_returns_entities_when_Owner_correct()
        {
            var forum = this.dbService.DbContext.Forums.First();

            var expectedResult = this.mapper.Map<ForumInputModel>(forum);

            var actualResult = this.forumService.GetMappedForumModel(forum).ForumModel;

            Assert.Equal(expectedResult.Name, actualResult.Name);
        }
    }
}