using AutoMapper;
using Forum.Models;
using Forum.Models.Enums;
using Forum.Services.Interfaces.Category;
using Forum.Services.Interfaces.Db;
using Forum.Services.Interfaces.Forum;
using Forum.ViewModels.Common;
using Forum.ViewModels.Forum;
using Forum.ViewModels.Interfaces.Forum;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Forum.Services.Forum
{
    public class ForumService : BaseService, IForumService
    {
        private readonly ICategoryService categoryService;

        public ForumService(IMapper mapper, IDbService dbService, ICategoryService categoryService)
            : base(mapper, dbService)
        {
            this.categoryService = categoryService;
        }

        public SubForum GetForum(string id, ModelStateDictionary modelState)
        {
            SubForum forum =
                this.dbService
                .DbContext
                .Forums
                .Where(f => f.Id == id)
                .Include(f => f.Category)
                .Include(f => f.Posts)
                .ThenInclude(f => f.Author)
                .FirstOrDefault();

            if (forum == null)
            {
                modelState.AddModelError("error", ErrorConstants.InvalidForumIdError);
            }

            return forum;
        }

        public IEnumerable<Models.Post> GetPostsByForum(string id, int start)
        {
            var forum = this.dbService.DbContext.Forums.FirstOrDefault(f => f.Id == id);

            var posts =
                forum
                .Posts
                .Skip(start)
                .Take(5)
                .OrderBy(p => p.StartedOn)
                .ToList() ?? new List<Models.Post>();

            return posts;
        }

        public int AddForum(IForumFormInputModel model, string categoryId)
        {
            var forum =
                  this.mapper
                  .Map<SubForum>(model);

            Models.Category category = this.categoryService.GetCategoryById(categoryId);

            forum.CreatedOn = DateTime.UtcNow;
            forum.CategoryId = categoryId;
            forum.Category = category;

            this.dbService.DbContext.Forums.Add(forum);
            return this.dbService.DbContext.SaveChanges();
        }

        public int Edit(IEditForumInputModel model, string forumId)
        {
            var forum = this.dbService.DbContext.Forums.FirstOrDefault(f => f.Id == forumId);
            var category = this.categoryService.GetCategoryById(model.Category);

            forum.Description = model.Description;
            forum.Name = model.Name;
            forum.Category = category;
            forum.CategoryId = category.Id;

            this.dbService.DbContext.Entry(forum).State = EntityState.Modified;
            return this.dbService.DbContext.SaveChanges();
        }

        public IForumFormInputModel GetMappedForumModel(SubForum forum)
        {
            var model = this.mapper.Map<ForumInputModel>(forum);

            var names = this.categoryService.GetAllCategories();

            var namesList =
                names
                .Select(x => new SelectListItem
                {
                    Value = x.Id,
                    Text = x.Name
                })
                .ToList();

            var forumFormModel = new ForumFormInputModel
            {
                ForumModel = model,
                Categories = namesList
            };

            return forumFormModel;
        }

        public int Delete(SubForum forum)
        {
            var forumPosts = this.dbService.DbContext.Posts.Where(p => p.ForumId == forum.Id).ToList();

            this.dbService.DbContext.RemoveRange(forumPosts);
            this.dbService.DbContext.Remove(forum);

            return this.dbService.DbContext.SaveChanges();
        }

        public IEnumerable<SubForum> GetAllForums(ClaimsPrincipal principal)

        {
            if (principal.IsInRole(Common.Role.Administrator) || principal.IsInRole(Common.Role.Owner))
            {
                var forums = this.dbService.DbContext.Forums.ToList();

                return forums;
            }
            else
            {
                var forums = this.dbService.DbContext.Forums
                    .Include(f => f.Category)
                    .Where(f => f.Category.Type != CategoryType.AdminOnly)
                    .ToList();

                return forums;
            }
        }
        public IEnumerable<string> GetForumPostsIds(string id)
        {
            var postsIds = this.GetPostsByForum(id, 0).Select(p => p.Id).ToList();

            return postsIds;
        }

        public bool ForumExists(string name)
        {
            var result = this.dbService.DbContext.Forums.Any(f => f.Name == name);

            return result;
        }

        public IEnumerable<string> GetAllForumsIds(ClaimsPrincipal principal, ModelStateDictionary modelState, string forumId)
        {
            var forums = this.GetAllForums(principal);
            if (forums == null)
            {
                modelState.AddModelError("error", ErrorConstants.InvalidPostIdError);

                return null;
            }
            else
            {
                var forumsIds = forums.Select(f => f.Id).ToList();
                if (!forumsIds.Contains(forumId))
                {
                    modelState.AddModelError("error", ErrorConstants.InvalidPostIdError);
                    return null;
                }
                else
                {
                    return forumsIds;
                }
            }
        }
    }
}