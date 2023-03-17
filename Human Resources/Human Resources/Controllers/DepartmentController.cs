using Human_Resources.Data;
using Human_Resources.Data.Services;
using Human_Resources.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Human_Resources.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _service;
        private readonly ILogger<DepartmentController> _logger;
        public DepartmentController(IDepartmentService service, ILogger<DepartmentController> logger)
        {
            _service = service;
            _logger = logger;
            
        }
        [HttpGet]
        public async  Task<IActionResult> Index()
        {
            var allBranches = await _service.GetAll();

            return View(allBranches);
        }
        [HttpGet]
        public IActionResult AddDepartment()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddDepartment([Bind("DepartmentName,DepartmentDescription")]Department department)
        {
            _logger.LogInformation(ModelState.IsValid.ToString());
            
            if (ModelState.IsValid)
            {
                _service.AddDepartment(department);
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
                _logger.LogInformation($"{department.Id},{department.DepartmentName},{department.DepartmentDescription}");
                return View(department);
            }
           

        }
        [HttpGet]
        public async Task<IActionResult> EditDepartment(int id)
        {
            var dept = await _service.GetById(id);
            if (dept == null)
            {
                return View("department not found");
            }
            else
            {
                return View(dept);
            }
        }
        [HttpPost]
        public IActionResult EditDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateDepartment(department);
                return RedirectToAction(nameof(EditDepartment));
        }
            return View(department);
    }
        [HttpGet]
        public IActionResult DeleteDepartment(int id)
        {
            var deleteValue = _service.GetById(id);
            if (deleteValue == null)
            {
                return View("Value not found");
            }
            else
            {
                return View(deleteValue);
            }
        }
        [HttpPost]
        public IActionResult DeleteDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
               _service.DeleteDepartment(department);
                return RedirectToAction(nameof(DeleteDepartment));
            }
            else
            {
                return View(department);
            }
        }

        

    }
}
