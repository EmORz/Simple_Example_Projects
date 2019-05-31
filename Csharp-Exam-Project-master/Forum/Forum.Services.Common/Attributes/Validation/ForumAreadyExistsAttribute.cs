using Forum.Services.Interfaces.Forum;
using Forum.ViewModels.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Forum.Services.Common.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ForumAreadyExistsAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var forumService = (IForumService)validationContext
                      .GetService(typeof(IForumService));

            var name = value.ToString();

            var forumExists = forumService.ForumExists(name);
            if (forumExists)
            {
                return new ValidationResult(ErrorConstants.ForumExistsError);
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}