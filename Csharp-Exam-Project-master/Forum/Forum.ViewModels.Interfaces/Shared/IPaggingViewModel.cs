using System.Collections.Generic;

namespace Forum.ViewModels.Interfaces.Shared
{
    public interface IPaggingViewModel
    {
        string EntityId { get; }

        string ControllerName { get; }

        string ControllerAction { get; }

        IEnumerable<string> Ids { get; }

        int PagesCount { get; }
    }
}