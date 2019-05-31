using System;
using Forum.MapConfiguration.Contracts;
using Forum.Models;
using Forum.ViewModels.Interfaces.Report;

namespace Forum.ViewModels.Report
{
    public class QuoteReportViewModel : IQuoteReportViewModel, IMapFrom<QuoteReport>
    {
        public string Id { get; set; }

        public string Description { get; set; }

        public ForumUser Author { get; set; }

        public string Title { get; set; }

        public Models.Quote Quote { get; set; }

        public DateTime ReportedOn { get; set; }
    }
}