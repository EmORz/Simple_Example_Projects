using Forum.MapConfiguration.Contracts;
using Forum.Models.Enums;

namespace Forum.ViewModels.Interfaces.Category
{
    public interface ICategoryInputModel : IMapTo<Models.Category>
    {
        string Name { get; set; }

        CategoryType Type { get; set; }
    }
}