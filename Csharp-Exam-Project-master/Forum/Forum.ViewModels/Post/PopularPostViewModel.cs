using AutoMapper;
using Forum.MapConfiguration.Contracts;
using Forum.ViewModels.Interfaces.Post;
using System;

namespace Forum.ViewModels.Post
{
    public class PopularPostViewModel : IPopularPostViewModel, IMapFrom<Models.Post>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Views { get; set; }
    }
}