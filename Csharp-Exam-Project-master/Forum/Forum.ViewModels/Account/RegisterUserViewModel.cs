using Forum.MapConfiguration.Contracts;
using Forum.Models;
using Forum.Services.Common.Attributes.Validation;
using Forum.ViewModels.Common;
using Forum.ViewModels.Interfaces.Account;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Forum.ViewModels.Account
{
    public class RegisterUserViewModel : IMapTo<ForumUser>, IRegisterUserViewModel
    {
        [Required(ErrorMessage = ErrorConstants.RequiredError)]
        [StringLength(ErrorConstants.MaximumNamesLength, ErrorMessage = ErrorConstants.StringLengthErrorMessage, MinimumLength = ErrorConstants.MinimumNamesLength)]
        [UsernameExists(ErrorConstants.AlreadyExistsError)]
        public string Username { get; set; }

        [Required(ErrorMessage = ErrorConstants.RequiredError)]
        [EmailAddress]
        [EmailExists(ErrorConstants.AlreadyExistsError)]
        public string Email { get; set; }

        [Required(ErrorMessage = ErrorConstants.RequiredError)]
        [StringLength(ErrorConstants.MaximumLocationLength, ErrorMessage = ErrorConstants.StringLengthErrorMessage, MinimumLength = ErrorConstants.MinimumLocationLength)]
        public string Location { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = ErrorConstants.RequiredError)]
        [Compare(nameof(ConfirmPassword), ErrorMessage = ErrorConstants.PasswordDontMatch)]
        [StringLength(ErrorConstants.MaximumPasswordsLength, ErrorMessage = ErrorConstants.StringLengthErrorMessage, MinimumLength = ErrorConstants.MinimumPasswordsLength)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = ErrorConstants.RequiredError)]
        [Compare(nameof(Password), ErrorMessage = ErrorConstants.PasswordDontMatch)]
        [StringLength(ErrorConstants.MaximumPasswordsLength, ErrorMessage = ErrorConstants.StringLengthErrorMessage, MinimumLength = ErrorConstants.MinimumPasswordsLength)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = ErrorConstants.RequiredError)]
        public string Gender { get; set; }
        
        [AllowedImageExtensions]
        public IFormFile Image { get; set; }
    }
}