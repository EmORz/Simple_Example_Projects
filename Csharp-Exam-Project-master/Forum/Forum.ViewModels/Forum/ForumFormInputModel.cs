using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Forum.ViewModels.Interfaces.Forum;
using Forum.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Forum.Services.Interfaces.Forum;
using Forum.Services.Interfaces.Category;
using Forum.ViewModels.Common;

namespace Forum.ViewModels.Forum
{
    public class ForumFormInputModel : IForumFormInputModel
    {
        public ForumFormInputModel()
        {
            this.ForumModel = new ForumInputModel();
        }

        public IForumInputModel ForumModel { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }
       
        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<ForumFormInputModel, SubForum>()
                   .ForMember(f => f.Description,
                       x => x.MapFrom(src => src.ForumModel.Description))
                   .ForMember(f => f.Name,
                       x => x.MapFrom(src => src.ForumModel.Name));
        }

        public IEnumerable<ValidationResult> Validate(System.ComponentModel.DataAnnotations.ValidationContext validationContext)
        {
            var forumService = (IForumService)validationContext
                           .GetService(typeof(IForumService));

            var categoryService = (ICategoryService)validationContext
                   .GetService(typeof(ICategoryService));

            var model = validationContext.ObjectInstance as ForumFormInputModel;

            var categoryResult = categoryService.IsCategoryValid(model.ForumModel.Category);

            var forumExists = forumService.ForumExists(model.ForumModel.Name);
            if (forumExists)
            {
                yield return new ValidationResult(ErrorConstants.ForumExistsError);
            }

            if (categoryResult)
            {
                yield return ValidationResult.Success;
            }
            else
            {
                yield return new ValidationResult(ErrorConstants.InvalidCategory);
            }
        }
    }
}