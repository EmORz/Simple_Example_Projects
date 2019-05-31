using AutoMapper;
using Forum.Models;
using Forum.Services.Interfaces.Db;
using Forum.Services.Interfaces.Report.Post;
using Forum.ViewModels.Common;
using Forum.ViewModels.Interfaces.Report;
using Forum.ViewModels.Report;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Forum.Services.Report.Post
{
    public class PostReportService : BaseService, IPostReportService
    {
        public PostReportService(IMapper mapper, IDbService dbService)
            : base(mapper, dbService)
        {
        }

        public IPostReportInputModel AddPostReport(IPostReportInputModel model, string authorId)
        {
            var report = this.mapper.Map<PostReport>(model);
            report.ReportedOn = DateTime.UtcNow;
            report.AuthorId = authorId;

            this.dbService.DbContext.PostReports.Add(report);
            this.dbService.DbContext.SaveChanges();

            return model;
        }

        public int DismissPostReport(string id, ModelStateDictionary modelState)
        {
            var report = this.dbService.DbContext.PostReports.Where(pr => pr.Id == id).FirstOrDefault();

            if (report == null)
            {
                modelState.AddModelError("error", ErrorConstants.InvalidPostReportIdError);
                return 0;
            }

            this.dbService.DbContext.PostReports.Remove(report);
            var result = this.dbService.DbContext.SaveChanges();

            return result;
        }

        public IEnumerable<IPostReportViewModel> GetPostReports(int start)
        {
            var reports =
                this.dbService
                .DbContext
                .PostReports
                .Include(pr => pr.Author)
                .Include(pr => pr.Post)
                .ThenInclude(pr => pr.Author)
                .OrderBy(pr => pr.ReportedOn)
                .Skip(start)
                .Take(5)
                .Select(pr => this.mapper.Map<PostReportViewModel>(pr))
                .ToList();

            return reports;
        }

        public int GetPostReportsCount()
        {
            var count = this.dbService.DbContext.PostReports.Count();

            return count;
        }
    }
}