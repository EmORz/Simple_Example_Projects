using System.Collections.Generic;
using Forum.ViewModels.Interfaces.Shared;

namespace Forum.ViewModels.Shared
{
    public class PaggingViewModel : IPaggingViewModel
    {
        public string EntityId { get; set; }

        public string ControllerName { get; set; }

        public string ControllerAction { get; set; }

        public IEnumerable<string> Ids { get; set; }

        public int PagesCount { get; set; }
    }
}