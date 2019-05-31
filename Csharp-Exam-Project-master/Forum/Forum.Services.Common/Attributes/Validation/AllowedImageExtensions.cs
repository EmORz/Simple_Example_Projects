using Forum.Services.Interfaces.Profile;
using Forum.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace Forum.Services.Common.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter)]
    public class AllowedImageExtensions : ValidationAttribute
    {
        public AllowedImageExtensions()
        {
        }

        public AllowedImageExtensions(string errorMessage) : base(errorMessage)
        {
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var image = value as IFormFile;
            if (image == null)
            {
                return ValidationResult.Success;
            }

            var manageAccountService = (IProfileService)validationContext
                   .GetService(typeof(IProfileService));

            var result = manageAccountService.IsImageExtensionValid(image.FileName);

            if (result)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorConstants.AllowedImgExtensionsError);
            }
        }
    }
}