using Forum.Models;
using Forum.ViewModels.Interfaces.Reply;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Forum.Services.Interfaces.Reply
{
    public interface IReplyService
    {
        Task<int> AddReply(IReplyInputModel model, ForumUser user);

        Models.Reply GetReply(string id, ModelStateDictionary modelState);

        int DeleteUserReplies(string username);

        IEnumerable<string> GetPostRepliesIds(string id);
    }
}