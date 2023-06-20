using Human_Resources.Data.Helpers;
using Human_Resources.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Human_Resources.Controllers
{
    public class ConfigurationController : Controller
    {
        private readonly IConfigurationService _service;
        public ConfigurationController(IConfigurationService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var configuration = await _service.GetById(1);
            return View(configuration);
        }
        [HttpGet]
        public async Task<IActionResult> EditConfiguration(int id)
        {
            var configuration = await _service.GetById(id);
            if(configuration != null)
            {
                return PartialView("~/Views/Shared/_EditConfiguration.cshtml",configuration);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditConfiguration(Configuration configuration)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateConfiguration(configuration);
                return RedirectToAction("Index");
            }
            else
            {
                return PartialView("~/Views/Shared/_EditConfiguration.cshtml", configuration);
            }
        }
    }
}
