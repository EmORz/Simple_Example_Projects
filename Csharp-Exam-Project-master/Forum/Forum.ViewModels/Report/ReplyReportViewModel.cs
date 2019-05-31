using Forum.MapConfiguration.Contracts;
using Forum.Models;
using Forum.ViewModels.Interfaces.Report;
using System;

namespace Forum.ViewModels.Report
{
    public class ReplyReportViewModel : IReplyReportViewModel, IMapFrom<ReplyReport>
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public ForumUser Author { get; set; }

        public string Title { get; set; }

        public Models.Reply Reply { get; set; }

        public DateTime ReportedOn { get; set; }
    }
}
