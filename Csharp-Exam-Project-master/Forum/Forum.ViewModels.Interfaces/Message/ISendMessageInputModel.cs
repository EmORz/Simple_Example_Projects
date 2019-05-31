using System.Collections.Generic;

namespace Forum.ViewModels.Interfaces.Message
{
    public interface ISendMessageInputModel
    {
        string Description { get; }

        string RecieverId { get; }

        string RecieverName { get; }

        IEnumerable<Models.Message> Messages { get; }

        bool ShowAll { get; }
    }
}