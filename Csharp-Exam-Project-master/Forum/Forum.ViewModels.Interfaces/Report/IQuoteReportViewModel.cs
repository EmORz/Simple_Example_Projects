using Forum.Models;
using System;

namespace Forum.ViewModels.Interfaces.Report
{
    public interface IQuoteReportViewModel
    {
        string Id { get; }

        string Description { get; }

        ForumUser Author { get; }

        string Title { get; }

        Models.Quote Quote { get; }

        DateTime ReportedOn { get; }
    }
}