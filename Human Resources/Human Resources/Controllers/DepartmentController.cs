using Human_Resources.Data;
using Human_Resources.Data.Services;
using Human_Resources.Models;
using Microsoft.AspNetCore.Mvc;

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
            var department = new Department();
            return View(department);
        }
        [HttpPost]
        public async Task<IActionResult> AddDepartment(Department department)
        {
            //if (Model.IsValid)
            //{
            await _service.AddDepartment(department);
            _logger.LogInformation("success");

            return RedirectToAction("index");
       // }
            //else
            //{
            //    _logger.LogInformation("failure");
            //    _logger.LogInformation($"{department.Id},{department.DepartmentName},{department.DepartmentDescription}");
            //    return View(department);
            //}
            
        }
        [HttpGet]
        public IActionResult EditDepartment(int id)
        {
            var dept = _service.GetById(id);
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
