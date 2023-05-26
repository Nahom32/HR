using Human_Resources.Data.Services;
using Human_Resources.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Human_Resources.Controllers
{
    [Authorize(Roles ="Admin,HRManager")]
    public class LeaveTypesController : Controller
    {
        private readonly ILeaveTypeService _service;
        private readonly ILogger<LeaveTypesController> _logger;
        public LeaveTypesController(ILeaveTypeService service, ILogger<LeaveTypesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var total = await _service.GetAll();
            return View(total);
        }
        [HttpGet]
        public IActionResult AddLeaveType()
        {
            return PartialView("~/Views/Shared/_AddLeaveType.cshtml", new LeaveTypes());
        }
        [HttpPost]
        public async Task<IActionResult> AddLeaveType(LeaveTypes leave)
        {
            if (ModelState.IsValid)
            {
                await _service.AddLeaveType(leave);
                return RedirectToAction("Index","LeaveTypes");
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError(error.ErrorMessage);
                }
                return PartialView("~/Views/Shared/_AddLeaveType.cshtml", leave);
            }

        }
        [HttpGet]
        public async Task<IActionResult> UpdateLeaveType(int id)
        {
            var model = await _service.GetById(id);
            if(model != null)
            {
                return PartialView("~/Views/Shared/_UpdateLeaveType.cshtml", model);
            }
            else
            {
                throw new Exception("The leave doesn't exist");
            }

        }
        [HttpPost]
        public async Task<IActionResult> UpdateLeaveType(LeaveTypes leave)
        {
            if(ModelState.IsValid)
            { 
                await _service.UpdateLeaveType(leave);
                return RedirectToAction("Index");
            }
            else
            {
                return PartialView("~/Views/Shared/_UpdateLeaveType.cshtml", leave);
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteLeaveType(int id)
        {
            var model = await _service.GetById(id);
            if (model != null)
            {
                return PartialView("~/Views/Shared/_DeleteLeaveType.cshtml",model);
            }
            else
            {
                throw new Exception("The leave doesn't exist");
            }

        }
        [HttpPost, ActionName("DeleteLeaveType")]
        public async Task<IActionResult> DeleteLeaveTypeConfirmed(int id)
        {
            var model = await _service.GetById(id);
            if (model != null)
            {
                await _service.DeleteLeaveType(id);
                return RedirectToAction("Index");
            }
            else
            {
                throw new Exception("The leave doesn't exist");
            }

        }

    }
}
