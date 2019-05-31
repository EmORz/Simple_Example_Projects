using Forum.MapConfiguration.Contracts;
using Forum.Services.Common.Attributes.Validation;
using Forum.ViewModels.Common;
using Forum.ViewModels.Interfaces.Post;
using System.ComponentModel.DataAnnotations;

namespace Forum.ViewModels.Post
{
    public class PostInputModel : IPostInputModel, IMapTo<Models.Post>
    {
        [PostAlreadyExists]
        [Required(ErrorMessage = ErrorConstants.RequiredError)]
        [RegularExpression(ModelsConstants.NamesRegex, ErrorMessage = ErrorConstants.NamesAllowedCharactersError)]
        [StringLength(ErrorConstants.MaximumNamesLength, ErrorMessage = ErrorConstants.StringLengthErrorMessage, MinimumLength = ErrorConstants.MinimumNamesLength)]
        public string Name { get; set; }

        [Required(ErrorMessage = ErrorConstants.RequiredError)]
        [StringLength(ErrorConstants.MaximumPostDescriptionLength, ErrorMessage = ErrorConstants.StringLengthErrorMessage, MinimumLength = ErrorConstants.MinimumPostDescriptionLength)]
        public string Description { get; set; }

        public string ForumId { get; set; }

        public string ForumName { get; set; }
    }
}