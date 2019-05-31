using Forum.MapConfiguration.Contracts;
using Forum.Models;
using Forum.ViewModels.Common;
using System.ComponentModel.DataAnnotations;

namespace Forum.ViewModels.Interfaces.Forum
{
    public interface IForumInputModel : IHaveCustomMappings, IMapTo<SubForum>
    {
        [Required(ErrorMessage = ErrorConstants.RequiredError)]
        [RegularExpression(ModelsConstants.NamesRegex, ErrorMessage = ErrorConstants.NamesAllowedCharactersError)]
        [StringLength(ErrorConstants.MaximumNamesLength, ErrorMessage = ErrorConstants.StringLengthErrorMessage, MinimumLength = ErrorConstants.MinimumNamesLength)]
        string Name { get; set; }

        [Required(ErrorMessage = ErrorConstants.RequiredError)]
        [RegularExpression(ModelsConstants.DescriptionsRegex, ErrorMessage = ErrorConstants.DescriptionsAllowedCharactersError)]
        [StringLength(ErrorConstants.MaximumForumDescriptionLength, ErrorMessage = ErrorConstants.StringLengthErrorMessage, MinimumLength = ErrorConstants.MinimumForumDescriptionLength)]
        string Description { get; set; }

        string Category { get; set; }
    }
}