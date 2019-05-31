namespace Forum.ViewModels.Interfaces.Report
{
    public interface IReplyReportInputModel
    {
        string Title { get; }

        string ReplyId { get; }

        string Description { get; }

        string PostId { get; }
    }
}