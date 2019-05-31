using Forum.Models;
using System;

namespace Forum.ViewModels.Interfaces.Report
{
    public interface IPostReportViewModel
    {
        string Id { get; }

        string Description { get; }

        ForumUser Author { get; }

        string Title { get; }

        Models.Post Post { get; }

        DateTime ReportedOn { get; }
    }
}