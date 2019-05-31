using AutoMapper;
using Forum.Models;
using Forum.Services.Interfaces.Category;
using Forum.Services.Interfaces.Db;
using Forum.ViewModels.Category;
using Forum.ViewModels.Interfaces.Category;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services.Category
{
    public class CategoryService : BaseService, ICategoryService
    {
        public CategoryService(IMapper mapper, IDbService dbService)
            : base(mapper, dbService)
        {
        }

        public async Task<int> AddCategory(ICategoryInputModel model, ForumUser user)
        {
            var category =
                this.mapper
                .Map<CategoryInputModel, Models.Category>(model as CategoryInputModel);

            category.CreatedOn = DateTime.UtcNow;
            category.User = user;
            category.UserId = user.Id;

            await this.dbService.DbContext.Categories.AddAsync(category);
            return await this.dbService.DbContext.SaveChangesAsync();
        }

        public IEnumerable<Models.Category> GetAllCategories()
        {
            var categories =
                this.dbService
                .DbContext
                .Categories
                .Include(c => c.Forums)
                .ThenInclude(c => c.Posts)
                .ToList();

            return categories;
        }

        public Models.Category GetCategoryById(string Id)
        {
            var category =
                this.dbService
                .DbContext
                .Categories
                .FirstOrDefault(c => c.Id == Id);

            return category;
        }

        public Models.Category GetCategoryByName(string name)
        {
            Models.Category category =
                this.dbService
                .DbContext
                .Categories
                .FirstOrDefault(c => c.Name == name);

            return category;
        }

        public IEnumerable<Models.Category> GetPublicCategories()
        {
            var categories =
                this.dbService
                .DbContext
                .Categories
                .Where(c => (int)c.Type != 2)
                .Include(c => c.Forums)
                .ThenInclude(c => c.Posts)
                .ToList();

            return categories;
        }

        public bool IsCategoryValid(string id)
        {
            var result =
                this.dbService
                .DbContext
                .Categories
                .Any(c => c.Id == id);

            return result;
        }
    }
}