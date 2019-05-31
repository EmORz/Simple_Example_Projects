using Forum.ViewModels.Interfaces.Home;
using Forum.ViewModels.Interfaces.Post;
using System.Collections.Generic;

namespace Forum.ViewModels.Home
{
    public class IndexInfoViewModel : IIndexInfoViewModel
    {
        public IEnumerable<Models.Category> Categories { get; set; }

        public int TotalUsersCount { get; set; }

        public int TotalPostsCount { get; set; }

        public string NewestUser { get; set; }

        public IEnumerable<ILatestPostViewModel> LatestPosts { get; set; }

        public IEnumerable<IPopularPostViewModel> PopularPosts { get; set; }
    }
}