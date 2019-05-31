using Forum.MapConfiguration.Contracts;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Forum.ViewModels.Interfaces.Forum
{
    public interface IForumFormInputModel : IHaveCustomMappings, IValidatableObject
    {
        IForumInputModel ForumModel { get; set; }

        IEnumerable<SelectListItem> Categories { get; set; }
    }
}