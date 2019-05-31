using System;
using AutoMapper;
using Forum.MapConfiguration.Contracts;
using Forum.ViewModels.Interfaces.Message;

namespace Forum.ViewModels.Message
{
    public class ChatMessageViewModel : IChatMessageViewModel, IHaveCustomMappings
    {
        public string AuthorName { get; set; }

        public string Description { get; set; }

        public string CreatedOn { get; set; }

        public string LoggedInUser { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Models.Message, ChatMessageViewModel>()
                .ForMember(dest => dest.AuthorName,
                x => x.MapFrom(src => src.Author.UserName))
                .ForMember(dest => dest.CreatedOn,
                x => x.MapFrom(src => src.CreatedOn.ToString()))
                .ForMember(dest => dest.Description,
                x => x.MapFrom(src => src.Description));
        }
    }
}