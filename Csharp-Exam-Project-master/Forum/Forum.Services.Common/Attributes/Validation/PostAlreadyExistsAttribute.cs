using Forum.Services.Interfaces.Db;
using Forum.ViewModels.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Forum.Services.Common.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PostAlreadyExistsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dbService = (IDbService)validationContext
                   .GetService(typeof(IDbService));

            var name = value.ToString();

            var post = dbService.DbContext.Posts.Any(p => p.Name == name);
            if (post)
            {
                return new ValidationResult(ErrorConstants.PostExistsError);
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}