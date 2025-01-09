using Core.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Presentation.UI.PanelUI.Controllers
{
    public class SetupRegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult SetupSettingSave([FromBody] SetupWorkplaceDtos setupWorkplaceDtos)
        {
            try
            {
                

                // İşlem başarılı
                return Json(new { success = true, message = "Workspace saved successfully." });
            }
            catch (Exception ex)
            {
                // Hata durumunda
                return Json(new { success = false, message = $"An error occurred: {ex.Message}" });
            }
        }

    }
}
