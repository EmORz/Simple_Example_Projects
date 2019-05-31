using Forum.Models;
using System.Threading.Tasks;

namespace Forum.Services.Forum.Contracts
{
    public interface IForumService
    {
        void Add(SubForum model, string category);

        Task<SubForum> GetPostsByForum(string id);

        Task<SubForum> GetForum(string id);
    }
}