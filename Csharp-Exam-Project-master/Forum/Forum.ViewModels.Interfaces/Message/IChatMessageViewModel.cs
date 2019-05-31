using System;

namespace Forum.ViewModels.Interfaces.Message
{
    public interface IChatMessageViewModel
    {
        string AuthorName { get; }

        string Description { get; }

        string CreatedOn { get; }

        string LoggedInUser { get; }
    }
}