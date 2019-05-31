using Forum.ViewModels.Interfaces.Report;
using System.Collections.Generic;

namespace Forum.Services.Interfaces.Report
{
    public interface IReportService
    {
        int DeleteUserReports(string username);
    }
}