using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Forum.Models;
using Forum.Services.Interfaces.Db;
using Forum.Services.Interfaces.Post;
using Forum.Services.Interfaces.Reply;
using Forum.ViewModels.Common;
using Forum.ViewModels.Interfaces.Reply;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Forum.Services.Reply
{
    public class ReplyService : BaseService, IReplyService
    {
        private readonly IPostService postService;

        public ReplyService(IMapper mapper, IDbService dbService, IPostService postService)
            : base(mapper, dbService)
        {
            this.postService = postService;
        }

        public async Task<int> AddReply(IReplyInputModel model, ForumUser user)
        {
            var reply = this.mapper.Map<Models.Reply>(model);

            reply.Author = user;
            reply.Description = this.postService.ParseDescription(reply.Description);
            reply.AuthorId = user.Id;
            reply.RepliedOn = DateTime.UtcNow;

            await this.dbService.DbContext.Replies.AddAsync(reply);
            var test = await this.dbService.DbContext.SaveChangesAsync();
            return test;
        }

        public int DeleteUserReplies(string username)
        {
            var userReplies =
                this.dbService
                .DbContext
                .Replies
                .Include(r => r.Author)
                .Where(r => r.Author.UserName == username)
                .ToList();

            this.dbService.DbContext.Replies.RemoveRange(userReplies);

            return this.dbService.DbContext.SaveChanges();
        }

        public IEnumerable<string> GetPostRepliesIds(string id)
        {
            var Ids =
                this.dbService
                .DbContext
                .Posts
                .Where(p => p.Id == id)
                .Include(p => p.Replies)
                .FirstOrDefault()
                .Replies
                .Select(r => r.Id)
                .ToList();

            return Ids;
        }

        public Models.Reply GetReply(string id, ModelStateDictionary modelState)
        {
            var reply =
                this.dbService
                .DbContext
                .Replies
                .Include(r => r.Author)
                .Include(r => r.Post)
                .FirstOrDefault(r => r.Id == id);

            if (reply == null)
            {
                modelState.AddModelError("error", ErrorConstants.InvalidReplyIdError);
            }

            return reply;
        }
    }
}