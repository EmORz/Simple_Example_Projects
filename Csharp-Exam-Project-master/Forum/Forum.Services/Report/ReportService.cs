using AutoMapper;
using Forum.Services.Interfaces.Db;
using Forum.Services.Interfaces.Report;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Forum.Services.Report
{
    public class ReportService : BaseService, IReportService
    {
        public ReportService(IMapper mapper, IDbService dbService)
            : base(mapper, dbService)
        {
        }

        public int DeleteUserReports(string username)
        {
            var postReports =
                this.dbService
                .DbContext
                .PostReports
                .Include(pr => pr.Author)
                .Where(pr => pr.Author.UserName == username)
                .ToList();

            this.dbService.DbContext.PostReports.RemoveRange(postReports);

            var replyReports =
                this.dbService
                .DbContext
                .ReplyReports
                .Include(pr => pr.Author)
                .Where(pr => pr.Author.UserName == username)
                .ToList();

            this.dbService.DbContext.ReplyReports.RemoveRange(replyReports);

            var quoteReports =
                this.dbService
                .DbContext
                .QuoteReports
                .Include(pr => pr.Author)
                .Where(pr => pr.Author.UserName == username)
                .ToList();

            this.dbService.DbContext.QuoteReports.RemoveRange(quoteReports);

            return this.dbService.DbContext.SaveChanges();
        }
    }
}