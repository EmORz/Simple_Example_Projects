using Forum.ViewModels.Interfaces.Report;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Forum.Services.Interfaces.Report.Quote
{
    public interface IQuoteReportService
    {
        IQuoteReportInputModel AddQuoteReport(IQuoteReportInputModel model, string authorId);

        IEnumerable<IQuoteReportViewModel> GetQuoteReports(int start);

        int DismissQuoteReport(string id, ModelStateDictionary modelState);

        int GetQuoteReportsCount();
    }
}