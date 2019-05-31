using Forum.Services.Interfaces.Category;
using System;
using System.ComponentModel.DataAnnotations;

namespace Forum.Services.Common.Attributes.Validation
{

    [AttributeUsage(AttributeTargets.Property)]
    public class CategoriesExistAttribute : ValidationAttribute
    {
        private ICategoryService categoryService;

        public CategoriesExistAttribute()
        {
        }

        public CategoriesExistAttribute(string errorMessage) : base(errorMessage)
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            this.categoryService = (ICategoryService)validationContext
                   .GetService(typeof(ICategoryService));

            if(this.categoryService.IsCategoryValid(value.ToString()))
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Invalid category.");
            }
        }
    }
}