using Forum.MapConfiguration.Contracts;
using Forum.Models;
using Forum.Services.Common.Attributes.Validation;
using Forum.ViewModels.Common;
using Forum.ViewModels.Interfaces.Account;
using System.ComponentModel.DataAnnotations;

namespace Forum.ViewModels.Account
{
    [UserExists(ErrorConstants.InvalidLoginAttempt)]
    public class LoginUserInputModel : IMapTo<ForumUser>, ILoginUserInputModel
    {
        [Required(ErrorMessage = ErrorConstants.RequiredError)]
        [StringLength(ErrorConstants.MaximumNamesLength, ErrorMessage = ErrorConstants.StringLengthErrorMessage, MinimumLength = ErrorConstants.MinimumNamesLength)]
        public string Username { get; set; }

        [Required(ErrorMessage = ErrorConstants.RequiredError)]
        [StringLength(ErrorConstants.MaximumPasswordsLength, ErrorMessage = ErrorConstants.StringLengthErrorMessage, MinimumLength = ErrorConstants.MinimumPasswordsLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}