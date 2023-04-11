using Human_Resources.Data;
using Human_Resources.Data.Services;
using Human_Resources.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Human_Resources.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EducationalFieldController : Controller
    {
        private readonly IEducationalFieldService _service;
        private readonly ILogger<EducationalFieldController> _logger;
        public EducationalFieldController(IEducationalFieldService service, ILogger<EducationalFieldController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allEducationalFields = await _service.GetAll();

            return View(allEducationalFields);
        }
        [HttpGet]
        public IActionResult AddEducationalField()
        {
            return PartialView("~/Views/Shared/_AddEducationalField.cshtml",new EducationalField());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEducationalField(EducationalField educationalField)
        {
            _logger.LogInformation(ModelState.IsValid.ToString());

            if (ModelState.IsValid)
            {
                await _service.AddEducationalField(educationalField);
                _logger.LogInformation("success");
                return RedirectToAction("index");
            }
            else
            {
                _logger.LogInformation("failure");
                var errorList = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .ToList();
                var errorString = string.Join("; ", errorList);
                _logger.LogInformation(errorString);
                
                return View(educationalField);
            }


        }
        [HttpGet]
        public async Task<IActionResult> EditEducationalField(int id)
        {
            var eduField = await _service.GetById(id);
            if (eduField == null)
            {
                return View("Educational Field not found");
            }
            else
            {
                return PartialView("~/Views/Shared/_EditEducationalField",eduField);
            }
        }
        [HttpPost]
        public IActionResult EditEducationalField(EducationalField educationalField)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("success");
                _service.UpdateEducationalField(educationalField);
                return RedirectToAction("Index");
            }
            _logger.LogInformation("failure");
            return View(educationalField);
        }
        [HttpGet]
        public async Task<IActionResult> DeleteEducationalField(int id)
        {
            var deleteValue = await _service.GetById(id);
            if (deleteValue == null)
            {
                return View("Value not found");
            }
            else
            {
                return PartialView("~/Views/Shared/_DeleteEducationalField",deleteValue);
            }
        }
        [HttpPost, ActionName("DeleteEducationalField")]
        public async Task<IActionResult> DeleteEducationalFieldConfirmed(int id)

        {
            var educationalField = await _service.GetById(id);
            if (educationalField != null)
            {
                _logger.LogInformation("success");
                _service.DeleteEducationalField(educationalField);
                return RedirectToAction("Index");
            }
            else
            {
                _logger.LogInformation("failed");
                var errorList = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .ToList();
                var errorString = string.Join("; ", errorList);
                _logger.LogInformation(errorString);
                return View(educationalField);
            }
        }



    }
}

