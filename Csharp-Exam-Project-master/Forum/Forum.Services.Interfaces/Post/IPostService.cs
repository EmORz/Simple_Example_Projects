using Forum.Models;
using Forum.ViewModels.Interfaces.Post;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Forum.Services.Interfaces.Post
{
    public interface IPostService
    {
        IEditPostInputModel GetEditPostModel(string Id, ClaimsPrincipal principal,ModelStateDictionary modelState);

        Task<int> AddPost(IPostInputModel model, ForumUser user, string forumId);

        IPostViewModel GetPost(string id, int start, ClaimsPrincipal principal, ModelStateDictionary modelState);

        string ParseDescription(string description);

        bool DoesPostExist(string Id);

        int GetTotalPostsCount();

        int ViewPost(string id);

        IEnumerable<ILatestPostViewModel> GetLatestPosts(ClaimsPrincipal principal);

        IEnumerable<IPopularPostViewModel> GetPopularPosts(ClaimsPrincipal principal);

        int Edit(IEditPostInputModel model, ClaimsPrincipal principal, ModelStateDictionary modelState);
    }
}