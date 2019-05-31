using Forum.MapConfiguration.Contracts;
using Forum.Models;
using Forum.Services.Interfaces.Db;
using Forum.Services.Interfaces.Post;
using Forum.ViewModels.Common;
using Forum.ViewModels.Interfaces.Report;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Forum.ViewModels.Report
{
    public class QuoteReportInputModel : IQuoteReportInputModel, IMapTo<QuoteReport>, IValidatableObject
    {
        [Required(ErrorMessage = ErrorConstants.RequiredError)]
        [MinLength(ErrorConstants.MinimumDescriptionLength, ErrorMessage = ErrorConstants.MinimumLengthError)]
        public string Description { get; set; }

        public string QuoteId { get; set; }

        public string PostId { get; set; }

        [Required(ErrorMessage = ErrorConstants.RequiredError)]
        [StringLength(ErrorConstants.MaximumNamesLength, ErrorMessage = ErrorConstants.StringLengthErrorMessage, MinimumLength = ErrorConstants.MinimumNamesLength)]
        public string Title { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var postService = (IPostService)validationContext
                   .GetService(typeof(IPostService));

            var dbService = (IDbService)validationContext
                .GetService(typeof(IDbService));

            var model = validationContext.ObjectInstance as QuoteReportInputModel;

            var quote = dbService.DbContext.Quotes.FirstOrDefault(q => q.Id == model.QuoteId);
            if (quote == null)
            {
                yield return new ValidationResult(ErrorConstants.InvalidQuoteIdError);
            }

            if (postService.DoesPostExist(model.PostId))
            {
                yield return ValidationResult.Success;
            }
            else
            {
                yield return new ValidationResult(ErrorConstants.InvalidPostIdError);
            }
        }
    }
}