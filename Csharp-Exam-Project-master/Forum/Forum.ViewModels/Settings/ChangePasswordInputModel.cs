using Forum.Services.Interfaces.Account;
using Forum.Services.Interfaces.Db;
using Forum.Services.Interfaces.Settings;
using Forum.ViewModels.Common;
using Forum.ViewModels.Interfaces.Settings;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Forum.ViewModels.Settings
{
    public class ChangePasswordInputModel : IChangePasswordInputModel
    {
        public string Username { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = ErrorConstants.RequiredError)]
        [StringLength(ErrorConstants.MaximumPasswordsLength, ErrorMessage = ErrorConstants.StringLengthErrorMessage, MinimumLength = ErrorConstants.MinimumPasswordsLength)]
        public string OldPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = ErrorConstants.RequiredError)]
        [StringLength(ErrorConstants.MaximumPasswordsLength, ErrorMessage = ErrorConstants.StringLengthErrorMessage, MinimumLength = ErrorConstants.MinimumPasswordsLength)]
        public string NewPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var settingsService = (ISettingsService)validationContext
                   .GetService(typeof(ISettingsService));

            var dbService = (IDbService)validationContext
                  .GetService(typeof(IDbService));
            
            var model = validationContext.ObjectInstance as ChangePasswordInputModel;

            var user = dbService.DbContext.Users.FirstOrDefault(u => u.UserName == model.Username);

            var result = settingsService.CheckPassword(user, model.OldPassword, new ModelStateDictionary());
            if (!result)
            {
                yield return new ValidationResult(ErrorConstants.IncorrectPasswordError);
            }
            else
            {
                yield return ValidationResult.Success;
            }
        }
    }
}