using System;
using System.Collections.Generic;
using Forum.MapConfiguration.Contracts;
using Forum.Models;
using Forum.ViewModels.Interfaces.Post;
using Forum.ViewModels.Interfaces.Quote;
using Forum.ViewModels.Interfaces.Reply;
using Forum.ViewModels.Quote;

namespace Forum.ViewModels.Post
{
    public class PostViewModel : IPostViewModel, IMapFrom<Models.Post>
    {
        public PostViewModel()
        {
            this.PostQuotes = new List<QuoteViewModel>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ForumUser Author { get; set; }

        public IEnumerable<Models.Reply> Replies { get; set; }

        public DateTime StartedOn { get; set; }

        public int Views { get; set; }

        public IReplyInputModel ReplyModel { get; set; }

        public IEnumerable<IQuoteViewModel> PostQuotes { get; set; }

        public int PagesCount { get; set; }
    }
}