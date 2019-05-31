using Forum.MapConfiguration.Contracts;
using Forum.ViewModels.Common;
using Forum.ViewModels.Interfaces.Quote;
using System.ComponentModel.DataAnnotations;

namespace Forum.ViewModels.Quote
{
    public class QuoteInputModel : IQuoteInputModel, IMapTo<Models.Quote>
    {
        public string Id { get; set; }

        [Required(ErrorMessage = ErrorConstants.RequiredError)]
        [StringLength(ErrorConstants.MaximumPostDescriptionLength, ErrorMessage = ErrorConstants.StringLengthErrorMessage, MinimumLength = ErrorConstants.MinimumPostDescriptionLength)]
        public string Description { get; set; }

        public string Quote { get; set; }

        public string RecieverId { get; set; }

        public string QuoteRecieverId { get; set; }

        public string ReplyId { get; set; }
    }
}