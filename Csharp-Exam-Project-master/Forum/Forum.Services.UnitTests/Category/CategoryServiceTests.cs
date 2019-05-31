using Forum.Models;
using Forum.Models.Enums;
using Forum.Services.Common;
using Forum.Services.Interfaces.Category;
using Forum.Services.Interfaces.Db;
using Forum.Services.UnitTests.Base;
using Forum.ViewModels.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Forum.Services.UnitTests.Category
{
    public class CategoryServiceTests : IClassFixture<BaseUnitTest>
    {
        private readonly IDbService dbService;

        private readonly ICategoryService categoryService;

        public CategoryServiceTests(BaseUnitTest fixture)
        {
            this.dbService = fixture.Provider.GetService(typeof(IDbService)) as IDbService;

            this.categoryService = fixture.Provider.GetService(typeof(ICategoryService)) as ICategoryService;

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

        private void SeedDb()
        {
            this.TruncateCategoriesTable();
            this.TruncateUsersTable();
            ForumUser user, secondUser;
            SeedUsers(out user, out secondUser);

            SeedCategories(user, secondUser);
        }

        private void SeedCategories(ForumUser user, ForumUser secondUser)
        {
            var category = new Models.Category { Id = TestsConstants.TestId, Name = TestsConstants.ValidCategoryName, User = user, UserId = user.Id, Type = CategoryType.AdminOnly };
            var secondCategory = new Models.Category { Id = TestsConstants.TestId1, Name = TestsConstants.ValidCategoryName1, User = secondUser, UserId = secondUser.Id, Type = CategoryType.Public };

            this.dbService.DbContext.Categories.Add(category);
            this.dbService.DbContext.Categories.Add(secondCategory);
            this.dbService.DbContext.SaveChanges();
        }

        private void SeedUsers(out ForumUser user, out ForumUser secondUser)
        {
            user = new ForumUser { Id = TestsConstants.TestId, UserName = TestsConstants.TestUsername1, RegisteredOn = DateTime.UtcNow.AddDays(1) };
            secondUser = new ForumUser { Id = TestsConstants.TestId2, Email = TestsConstants.TestEmail, UserName = TestsConstants.TestUsername2, RegisteredOn = DateTime.UtcNow.AddDays(2) };
            var thirdUser = new ForumUser { Id = TestsConstants.TestId3, UserName = TestsConstants.TestUsername3, RegisteredOn = DateTime.UtcNow.AddDays(3) };

            this.dbService.DbContext.Users.Add(user);
            this.dbService.DbContext.Users.Add(secondUser);
            this.dbService.DbContext.Users.Add(thirdUser);
            this.dbService.DbContext.SaveChanges();
        }

        [Fact]
        public void GetCategoryByName_returns_entity_when_correct()
        {
            var expectedResult = TestsConstants.ValidCategoryName;

            var actualResult = this.categoryService.GetCategoryByName(TestsConstants.ValidCategoryName).Name;

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetCategoryByName_returns_null_when_correct()
        {
            Assert.Null(this.categoryService.GetCategoryByName(TestsConstants.ValidCategoryName2));
        }

        [Fact]
        public void GetCategoryById_returns_entity_when_correct()
        {
            var expectedResult = TestsConstants.TestId;
            var actualResult = this.categoryService.GetCategoryById(TestsConstants.TestId).Id;

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public void GetCategoryById_returns_null_when_correct()
        {
            Assert.Null(this.categoryService.GetCategoryById(Guid.NewGuid().ToString()));
        }

        [Fact]
        public void AddCategory_returns_two_when_correct()
        {
            var inputModel = new CategoryInputModel { Name = TestsConstants.ValidCategoryName2, Type = CategoryType.Public };

            var user = this.dbService.DbContext.Users.FirstOrDefault(u => u.Id == TestsConstants.TestId);

            var actualResult = this.categoryService.AddCategory(inputModel, user).GetAwaiter().GetResult();

            Assert.Equal(1, actualResult);
        }

        [Fact]
        public void GetAllCategories_returns_entities_when_correct()
        {
            this.TruncateCategoriesTable();
            this.TruncateUsersTable();

            this.SeedDb();

            var expectedList = new List<string> { TestsConstants.TestId, TestsConstants.TestId1 }.OrderBy(c => c);

            Assert.Equal(expectedList, this.categoryService.GetAllCategories().Select(c => c.Id).OrderBy(c => c));
        }

        [Fact]
        public void GetAllCategories_returns_empty_list_when_correct()
        {
            this.TruncateCategoriesTable();
            this.TruncateUsersTable();

            var expectedList = new List<Models.Category>();

            Assert.Equal(expectedList, this.categoryService.GetAllCategories());

            this.SeedDb();
        }

        [Fact]
        public void GetPublicCategories_returns_entities_when_correct()
        {
            var expectedList = new List<string> { TestsConstants.ValidCategoryName1};

            Assert.Equal(expectedList, this.categoryService.GetPublicCategories().Select(c => c.Name));
        }       

        [Fact]
        public void IsCategoryValid_returns_true_when_correct()
        {
            var actualResult = this.categoryService.IsCategoryValid(TestsConstants.TestId);

            Assert.True(actualResult == true);
        }

        [Fact]
        public void IsCategoryValid_returns_false_when_incorrect()
        {
            var result = this.categoryService.IsCategoryValid(Guid.NewGuid().ToString());

            Assert.True(result == false);
        }
    }
}