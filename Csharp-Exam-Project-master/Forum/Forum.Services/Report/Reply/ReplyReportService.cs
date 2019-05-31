using AutoMapper;
using Forum.Models;
using Forum.Services.Interfaces.Db;
using Forum.Services.Interfaces.Report.Reply;
using Forum.ViewModels.Common;
using Forum.ViewModels.Interfaces.Report;
using Forum.ViewModels.Report;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Forum.Services.Report.Reply
{
    public class ReplyReportService : BaseService, IReplyReportService
    {
        public ReplyReportService(IMapper mapper, IDbService dbService)
            : base(mapper, dbService)
        {
        }

        public IReplyReportInputModel AddReplyReport(IReplyReportInputModel model, string authorId)
        {
            var report = this.mapper.Map<ReplyReport>(model);
            report.ReportedOn = DateTime.UtcNow;
            report.AuthorId = authorId;

            this.dbService.DbContext.ReplyReports.Add(report);
            this.dbService.DbContext.SaveChanges();

            return model;
        }

        public IEnumerable<IReplyReportViewModel> GetReplyReports(int start)
        {
            var reports =
                   this.dbService
                   .DbContext
                   .ReplyReports
                   .Include(rr => rr.Author)
                   .Include(rr => rr.Reply)
                   .ThenInclude(rr => rr.Author)
                   .OrderBy(rr => rr.ReportedOn)
                   .Skip(start)
                   .Take(5)
                   .Select(rr => this.mapper.Map<ReplyReportViewModel>(rr))
                   .ToList();

            return reports;
        }

        public int DismissReplyReport(string id, ModelStateDictionary modelState)
        {
            var report = this.dbService.DbContext.ReplyReports.Where(pr => pr.Id == id).FirstOrDefault();

            if (report == null)
            {
                modelState.AddModelError("error", ErrorConstants.InvalidReplyReportIdError);
                return 0;
            }

            this.dbService.DbContext.ReplyReports.Remove(report);
            var result = this.dbService.DbContext.SaveChanges();

            return result;
        }

        public int GetReplyReportsCount()
        {
            var count = this.dbService.DbContext.ReplyReports.Count();

            return count;
        }
    }
}