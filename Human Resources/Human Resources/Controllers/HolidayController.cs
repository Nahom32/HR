using Human_Resources.Data.Services;
using Human_Resources.Models;
using Microsoft.AspNetCore.Mvc;

namespace Human_Resources.Controllers
{
    public class HolidayController : Controller
    {
        private readonly IHolidayService _service;
        public HolidayController(IHolidayService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var all = await _service.GetAll();
            return View(all);
        }
        [HttpGet]
        public  IActionResult AddHoliday()
        {
            return PartialView("~/Views/Shared/_AddHoliday.cshtml", new Holiday());
        }
        [HttpPost]
        public async Task<IActionResult> AddHoliday(Holiday holiday)
        {
            if(ModelState.IsValid)
            {
                await _service.AddHoliday(holiday);
                return RedirectToAction("Index");
            }
            else
            {
                return PartialView("~/Views/Shared/_AddHoliday.cshtml", holiday);
            }
        }
        [HttpGet]
        public async Task<IActionResult> UpdateHoliday(int id)
        {
            var holiday = await _service.GetById(id);
            if(holiday != null)
            {
                return PartialView("~/Views/Shared/_EditHoliday.cshtml", holiday);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateHoliday(Holiday holiday)
        {
            if(ModelState.IsValid)
            {
                await _service.UpdateHoliday(holiday);
                return RedirectToAction("Index");
            }
            else
            {
                return PartialView("~/Views/Shared/_EditHoliday.cshtml", holiday);
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteHoliday(int id)
        {
            var holiday = await _service.GetById(id);
            if(holiday != null)
            {
                return PartialView("~/Views/Shared/_DeleteHoliday.cshtml", holiday);

            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost, ActionName("DeleteHoliday")]
        public async Task<IActionResult> DeleteHolidayConfirmed(int id)
        {
            var holiday = await _service.GetById(id);
            if (holiday != null)
            {
                await _service.DeleteHoliday(holiday);
                return RedirectToAction("Index");

            }
            else
            {
                return NotFound();
            }

        }

    }
}
