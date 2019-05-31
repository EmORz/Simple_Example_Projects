using Forum.Services.Interfaces.Category;
using Forum.ViewModels.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Forum.Services.Common.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class CategoryNameExistsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var categoryService = (ICategoryService)validationContext
                   .GetService(typeof(ICategoryService));

            var name = value.ToString();

            var category = categoryService.GetCategoryByName(name);
            if (category != null)
            {
                return new ValidationResult(ErrorConstants.CategoryExistsError);
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}