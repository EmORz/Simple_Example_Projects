using Forum.Models;
using Forum.Services.Interfaces.Post;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Reflection;

namespace Forum.Web.Filters
{
    public class ViewsFilter : ResultFilterAttribute
    {
        private readonly IPostService postService;

        public ViewsFilter(IPostService postService)
        {
            this.postService = postService;
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            var id = context.HttpContext.Request.Query["id"];

            var post = this.postService.ViewPost(id);
        }
    }
}