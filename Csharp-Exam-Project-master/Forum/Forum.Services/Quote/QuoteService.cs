using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Forum.MapConfiguration.Contracts;
using Forum.Models;
using Forum.Services.Common;
using Forum.Services.Interfaces.Db;
using Forum.ViewModels.Common;
using Forum.ViewModels.Interfaces.Quote;
using Forum.ViewModels.Quote;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Forum.Services.Quote
{
    [Authorize]
    public class QuoteService : BaseService, Interfaces.Quote.IQuoteService, IMapTo<Models.Quote>
    {
        public QuoteService(IMapper mapper, IDbService dbService)
            : base(mapper, dbService)
        {
        }

        public int AddQuote(IQuoteInputModel model, ForumUser user, string recieverName)
        {
            var quote = this.mapper.Map<Models.Quote>(model);
            quote.Id = Guid.NewGuid().ToString();
            quote.Author = user;
            quote.AuthorId = user.Id;
            quote.QuotedOn = DateTime.UtcNow;

            quote.Description = String.Format(ServicesConstants.ReplyingTo, recieverName) + quote.Description;

            this.dbService.DbContext.Quotes.Add(quote);
            return this.dbService.DbContext.SaveChanges();
        }

        public int DeleteUserQuotes(ForumUser user)
        {
            var quotes =
                this.dbService
                .DbContext
                .Quotes
                .Where(q => q.AuthorId == user.Id)
                .ToList();

            this.dbService.DbContext.RemoveRange(quotes);
            return this.dbService.DbContext.SaveChanges();
        }

        public Models.Quote GetQuote(string id, ModelStateDictionary modelState)
        {
            var quote =
                this.dbService
                .DbContext
                .Quotes
                .Include(q => q.Author)
                .Include(q => q.Reply)
                .ThenInclude(q => q.Author)
                .Include(q => q.Reply.Post)
                .FirstOrDefault(q => q.Id == id);

            if (quote == null)
            {
                modelState.AddModelError("error", ErrorConstants.InvalidQuoteIdError);
            }

            return quote;
        }

        public IEnumerable<IQuoteViewModel> GetQuotesByPost(string id)
        {
            var quotes =
                this.dbService
                .DbContext
                .Quotes
                .Include(q => q.Author)
                .ThenInclude(q => q.Posts)
                .Include(q => q.Reply)
                .Where(q => q.Reply.PostId == id)
                .Select(q => mapper.Map<QuoteViewModel>(q));

            return quotes;
        }
    }
}