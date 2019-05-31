using AutoMapper;
using Forum.Services.Common.Attributes.Validation;
using Forum.ViewModels.Common;
using Forum.ViewModels.Interfaces.Settings;
using System.ComponentModel.DataAnnotations;

namespace Forum.ViewModels.Settings
{
    public class EditProfileInputModel : IEditProfileInputModel
    {
        [Required(ErrorMessage = ErrorConstants.RequiredError)]
        [StringLength(ErrorConstants.MaximumNamesLength, ErrorMessage = ErrorConstants.StringLengthErrorMessage, MinimumLength = ErrorConstants.MinimumNamesLength)]
        [UsernameExists(ErrorMessage = ErrorConstants.AlreadyExistsError)]
        public string Username { get; set; }

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

        [Required(ErrorMessage = ErrorConstants.MustEnterValidValueError)]
        public string Gender { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Models.ForumUser, EditProfileInputModel>()
                .ForMember(dest => dest.Username,
                    x => x.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Gender,
                    x => x.MapFrom(src => src.Gender))
                    .ForMember(dest => dest.Location,
                    x => x.MapFrom(src => src.Location));
        }
    }
}