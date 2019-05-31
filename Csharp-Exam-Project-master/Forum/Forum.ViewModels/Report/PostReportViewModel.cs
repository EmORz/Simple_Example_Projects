using System;
using Forum.MapConfiguration.Contracts;
using Forum.Models;
using Forum.ViewModels.Interfaces.Report;

namespace Forum.ViewModels.Report
{
    public class PostReportViewModel : IPostReportViewModel, IMapFrom<PostReport>
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public ForumUser Author { get; set; }

        public string Title { get; set; }

        public Models.Post Post { get; set; }

        public DateTime ReportedOn { get; set; }
    }
}