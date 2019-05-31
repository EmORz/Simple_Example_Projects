using AutoMapper;
using Forum.Models;
using Forum.ViewModels.Interfaces.Forum;

namespace Forum.ViewModels.Forum
{
    public class ForumInputModel : IForumInputModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }
        
        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<SubForum, ForumInputModel>()
                .ForMember(fm => fm.Category,
                x => x.MapFrom(src => src.Category.Name))
                .ForMember(fm => fm.Name,
                x => x.MapFrom(src => src.Name))
                .ForMember(fm => fm.Description,
                x => x.MapFrom(src => src.Description));
        }
    }
}