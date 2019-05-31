namespace Forum.Services.Category.Contracts
{
    using global::Forum.Models;
    using System.Threading.Tasks;

    public interface ICategoryService
    {
        Category GetCategoryByName(string name);

        Category GetCategoryById(string Id);

        Task<int> AddCategory(Category model, ForumUser user);

        Category[] GetAllCategories();

        Category[] GetUsersCategories();

        bool IsCategoryValid(string name);

    }
}