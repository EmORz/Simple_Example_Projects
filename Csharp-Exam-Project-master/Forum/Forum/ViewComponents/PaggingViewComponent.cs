using Forum.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Forum.Web.ViewComponents
{
    [ViewComponent(Name = "Pagging")]
    public class PaggingViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(string controller, string action, IEnumerable<string> Ids, int PagesCount, string entityId)
        {
            if (controller == "Forum")
            {
                return this.View("Pagging", new PaggingViewModel { ControllerName = controller, ControllerAction = action, Ids = Ids, PagesCount = PagesCount, EntityId = entityId });
            }
            else if (controller == "Post")
            {
                return this.View("Pagging", new PaggingViewModel { ControllerName = controller, ControllerAction = action, Ids = Ids, PagesCount = PagesCount, EntityId = entityId });
            }

            return null;
        }
    }
}