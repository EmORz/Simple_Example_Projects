using Forum.Models;
using System;

namespace Forum.ViewModels.Interfaces.Report
{
    public interface IReplyReportViewModel
    {
        string Id { get; }

        string Description { get; }

        ForumUser Author { get; }

        string Title { get; }

        Models.Reply Reply { get; }

        DateTime ReportedOn { get; }
    }
}