using Forum.ViewModels.Interfaces.Report;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace Forum.Services.Interfaces.Report.Reply
{
    public interface IReplyReportService
    {
        IReplyReportInputModel AddReplyReport(IReplyReportInputModel model, string authorId);

        IEnumerable<IReplyReportViewModel> GetReplyReports(int start);

        int DismissReplyReport(string id, ModelStateDictionary modelState);

        int GetReplyReportsCount();
    }
}