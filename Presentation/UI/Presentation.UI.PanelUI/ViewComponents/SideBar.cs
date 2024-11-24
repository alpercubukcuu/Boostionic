using Microsoft.AspNetCore.Mvc;

namespace Presentation.UI.PanelUI.ViewComponents
{
    public class Sidebar : ViewComponent
    {
        public IViewComponentResult Invoke(string viewName)
        {
            return View(viewName);
        }
    }
}
