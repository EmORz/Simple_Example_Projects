using Forum.Models;
using Forum.ViewModels.Interfaces.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forum.Services.Interfaces.Category
{
    public interface ICategoryService
    {
        Models.Category GetCategoryByName(string name);

        Models.Category GetCategoryById(string Id);

        Task<int> AddCategory(ICategoryInputModel model, ForumUser user);

        IEnumerable<Models.Category> GetAllCategories();

        IEnumerable<Models.Category> GetPublicCategories();

        bool IsCategoryValid(string name);
    }
}