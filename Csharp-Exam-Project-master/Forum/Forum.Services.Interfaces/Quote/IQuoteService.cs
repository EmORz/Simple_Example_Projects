using Forum.Models;
using Forum.ViewModels.Interfaces.Quote;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Forum.Services.Interfaces.Quote
{
    public interface IQuoteService
    {
        int AddQuote(IQuoteInputModel model, ForumUser user, string recieverName);

        IEnumerable<IQuoteViewModel> GetQuotesByPost(string id);

        Models.Quote GetQuote(string id, ModelStateDictionary modelState);

        int DeleteUserQuotes(ForumUser user);
    }
}