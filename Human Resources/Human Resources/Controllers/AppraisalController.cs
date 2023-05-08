using Human_Resources.Data.Services;
using Human_Resources.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Human_Resources.Controllers
{
    public class AppraisalController : Controller
    {
        private readonly IAppraisalService _service;
        public AppraisalController(IAppraisalService service)
        {
            _service = service;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var Appraisals = await _service.GetAll();
            return View(Appraisals);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddAppraisal(int EmployeeId)
        {
            AppraisalViewModel appraisal = new AppraisalViewModel();
            appraisal.EmployeeId = EmployeeId;
            var Employeedropdowns = await _service.GetEmployeedropdowns();
            ViewBag.Employees = new SelectList(Employeedropdowns.Employees, "Id", "Name");
            return View(appraisal);

        }
        [HttpPost]
        
        public async Task<IActionResult> AddAppraisal(AppraisalViewModel appraisal)
        {
            if(!ModelState.IsValid)
            {
                var Employeedropdowns = await _service.GetEmployeedropdowns();
                ViewBag.Employees = new SelectList(Employeedropdowns.Employees, "Id", "Name");
                return View(appraisal);
            }
            else
            {
                await _service.AddAppraisal(appraisal);
                return RedirectToAction("Index", "Appraisal");
            }
        }
        [HttpGet]
        [Authorize(Roles ="User")]
        public async Task<IActionResult> EditAppraisal(int id)
        {
            var appraisal = await _service.GetById(id);
            var Employeedropdowns = await _service.GetEmployeedropdowns();
            ViewBag.Employees = new SelectList(Employeedropdowns.Employees, "Id", "Name");
            var appraisalVm = new AppraisalViewModel()
            {
                Id = appraisal.Id,
                EmployeeId = appraisal.EmployeeId,
                Timeliness = appraisal.Timeliness,
                GroupWork = appraisal.GroupWork,
                TechnicalSkills = appraisal.TechnicalSkills,
                Punctuality = appraisal.Punctuality,
                CollaborativeSkills = appraisal.CollaborativeSkills
            };
            return View(appraisalVm);
        }
        [HttpPost]
        public async Task<IActionResult> EditAppraisal(AppraisalViewModel appraisal)
        {
            if(!ModelState.IsValid)
            {
                var Employeedropdowns = await _service.GetEmployeedropdowns();
                ViewBag.Employees = new SelectList(Employeedropdowns.Employees, "Id", "Name");
                return View(appraisal);
            }
            else
            {
                await _service.UpdateAppraisal(appraisal);
                return RedirectToAction("Index", "Appraisal");
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteAppraisal(int id)
        {
            var appraisal = await _service.GetById(id);
            var Employeedropdowns = await _service.GetEmployeedropdowns();
            ViewBag.Employees = new SelectList(Employeedropdowns.Employees, "Id", "Name");
            var appraisalVm = new AppraisalViewModel()
            {
                Id = appraisal.Id,
                EmployeeId = appraisal.EmployeeId,
                Timeliness = appraisal.Timeliness,
                GroupWork = appraisal.GroupWork,
                TechnicalSkills = appraisal.TechnicalSkills,
                Punctuality = appraisal.Punctuality,
                CollaborativeSkills = appraisal.CollaborativeSkills
            };
            return View(appraisalVm);
        }
        [HttpPost, ActionName("DeleteAppraisal")]
        public async Task<IActionResult> DeleteAppraisalConfirmed(int id)
        {
            var appraisal = await _service.GetById(id);
            if (appraisal != null)
            {
                var appraisalVm = new AppraisalViewModel()
                {
                    Id = appraisal.Id,
                    EmployeeId = appraisal.EmployeeId,
                    Timeliness = appraisal.Timeliness,
                    GroupWork = appraisal.GroupWork,
                    TechnicalSkills = appraisal.TechnicalSkills,
                    Punctuality = appraisal.Punctuality,
                    CollaborativeSkills = appraisal.CollaborativeSkills
                };
                await _service.DeleteAppraisal(appraisalVm);
                return RedirectToAction("Index", "Appraisal");
            }
            else
            {
                return View("The Appraisal doesn't exist");
            }
        }
        [HttpGet]
        [Authorize(Roles="Admin,User")]
        public async Task<IActionResult> Details(int id)
        {
            var Result = await _service.GetById(id);
            if (Result != null)
            {
                return View(Result);
            }
            else
            {
                throw new Exception("The appraisal record wasn't found");
            }
        }
    }
}
