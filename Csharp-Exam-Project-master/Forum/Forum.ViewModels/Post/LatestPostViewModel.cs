using AutoMapper;
using Forum.MapConfiguration.Contracts;
using Forum.ViewModels.Interfaces.Post;
using System;

namespace Forum.ViewModels.Post
{
    public class LatestPostViewModel : ILatestPostViewModel, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string AuthorName { get; set; }

        public string StartedOn { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Models.Post, LatestPostViewModel>()
                .ForMember(dest => dest.Id,
                x => x.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name,
                x => x.MapFrom(src => src.Name))
                .ForMember(dest => dest.AuthorName,
                x => x.MapFrom(src => src.Author.UserName))
                .ForMember(dest => dest.StartedOn,
                x => x.MapFrom(src => src.StartedOn.ToString("dd-MM-yyyy")));
        }
    }
}