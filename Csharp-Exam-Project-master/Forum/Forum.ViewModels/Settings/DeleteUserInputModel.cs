using Forum.Services.Interfaces.Account;
using Forum.ViewModels.Common;
using Forum.ViewModels.Interfaces.Settings;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Forum.ViewModels.Settings
{
    public class DeleteUserInputModel : IDeleteUserInputModel
    {
        [Required(ErrorMessage = ErrorConstants.RequiredError)]
        [StringLength(ErrorConstants.MaximumNamesLength, ErrorMessage = ErrorConstants.StringLengthErrorMessage, MinimumLength = ErrorConstants.MinimumNamesLength)]
        public string Username { get; set; }

        [Required(ErrorMessage = ErrorConstants.RequiredError)]
        [StringLength(ErrorConstants.MaximumPasswordsLength, ErrorMessage = ErrorConstants.StringLengthErrorMessage, MinimumLength = ErrorConstants.MinimumPasswordsLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var accountService = (IAccountService)validationContext
                  .GetService(typeof(IAccountService));

            var model = validationContext.ObjectInstance as DeleteUserInputModel;

            var result = accountService.UserWithPasswordExists(model.Username, model.Password).GetAwaiter().GetResult();
            if (!result)
            {
                yield return new ValidationResult(ErrorConstants.UserNotFoundError);
            }
            else
            {
                yield return ValidationResult.Success;
            }
        }
    }
}