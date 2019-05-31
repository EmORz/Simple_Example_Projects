namespace Forum.ViewModels.Interfaces.Report
{
    public interface IPostReportInputModel
    {
        string Title { get; }

        string Description { get; }

        string PostId { set; }
    }
}