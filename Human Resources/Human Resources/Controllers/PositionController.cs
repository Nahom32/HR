using Human_Resources.Data.Services;
using Human_Resources.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Human_Resources.Controllers
{
    [Authorize(Roles ="Admin")]
    public class PositionController : Controller
    {
        private readonly IPositionService _service;
        private readonly ILogger<PositionController> _logger;
        public PositionController(IPositionService service, ILogger<PositionController> logger)
        {
            _service = service;
            _logger = logger;

        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allPositions = await _service.GetAll();

            return View(allPositions);
        }
        [HttpGet]
        public IActionResult AddPosition()
        {

            return PartialView("~/Views/Shared/_AddPosition.cshtml",new Position());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPosition(Position position)
        {
            _logger.LogInformation(ModelState.IsValid.ToString());

            if (ModelState.IsValid)
            {
                await _service.AddPosition(position);
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
                return View(position);
            }


        }
        [HttpGet]
        public async Task<IActionResult> EditPosition(int id)
        {
            var position = await _service.GetById(id);
            if (position == null)
            {
                return View("Position not found");
            }
            else
            {
                return PartialView("~/Views/Shared/_EditPosition.cshtml",position);
            }
        }
        [HttpPost]
        public IActionResult EditPosition(Position position)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("success");
                _service.UpdatePosition(position);
                return RedirectToAction("Index");
            }
            _logger.LogInformation("failure");
            return View(position);
        }
        [HttpGet]
        public async Task<IActionResult> DeletePosition(int id)
        {
            var deleteValue = await _service.GetById(id);
            if (deleteValue == null)
            {
                return View("Value not found");
            }
            else
            {
                return PartialView("~/Views/Shared/_EditPosition.cshtml", deleteValue);
            }
        }
        [HttpPost, ActionName("DeletePosition")]
        public async Task<IActionResult> DeletePositionConfirmed(int id)
        {
            var position = await _service.GetById(id);
            if (position != null)
            {
                _logger.LogInformation("success");
                _service.DeletePosition(position);
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
                return View(position);
            }
        }
    }
}
