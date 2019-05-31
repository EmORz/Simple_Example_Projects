using Forum.Models;
using Forum.ViewModels.Interfaces.Quote;
using Forum.ViewModels.Interfaces.Reply;
using System;
using System.Collections.Generic;

namespace Forum.ViewModels.Interfaces.Post
{
    public interface IPostViewModel
    {
        string Id { get; set; }

        string Name { get; set; }

        DateTime StartedOn { get; set; }

        int Views { get; set; }

        string Description { get; set; }

        ForumUser Author { get; set; }

        IEnumerable<Models.Reply> Replies { get; set; }

        IReplyInputModel ReplyModel { get; set; }

        IEnumerable<IQuoteViewModel> PostQuotes { get; set; }

        int PagesCount { get; set; }
    }
}