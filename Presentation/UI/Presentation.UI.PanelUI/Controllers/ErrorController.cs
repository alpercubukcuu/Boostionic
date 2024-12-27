using Microsoft.AspNetCore.Mvc;

namespace Presentation.UI.PanelUI.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult ErrorPage(string? errorMessage)
        {
            ViewBag.ErrorMessage = errorMessage;

            return View();
        }
    }
}
