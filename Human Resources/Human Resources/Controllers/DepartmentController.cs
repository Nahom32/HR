using Human_Resources.Data;
using Human_Resources.Data.Services;
using Human_Resources.Data.ViewModels;
using Human_Resources.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Logging;

namespace Human_Resources.Controllers
{
    [Authorize(Roles ="Admin,User,HRAdmin")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _service;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IEmployeeService _employeeService;
        public DepartmentController(IDepartmentService service, ILogger<DepartmentController> logger, IEmployeeService employeeService)
        {
            _service = service;
            _logger = logger;
            _employeeService = employeeService;
        }
        [HttpGet]
        public async  Task<IActionResult> Index()
        {
            var departments = await _service.GetAll();
            return View(departments);
        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public IActionResult AddDepartment()
        {
            //DepartmentViewModel departmentViewModel = new DepartmentViewModel();
            return PartialView("~/Views/Shared/_AddDepartment.cshtml",new Department());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDepartment(Department department)
        {
            _logger.LogInformation(ModelState.IsValid.ToString());
            
            if (ModelState.IsValid)
            {
                await _service.AddDepartment(department);
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
                return PartialView("_AddDepartment",department);
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
                return PartialView("_EditDepartment",dept);
            }
        }
        [HttpPost]
        public IActionResult EditDepartment(Department department)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("success");
                _service.UpdateDepartment(department);
                return RedirectToAction("Index");
            }
            _logger.LogInformation("failure");
            return PartialView("_EditDepartment",department);
    }
        [HttpGet]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            var deleteValue = await _service.GetById(id);
            if (deleteValue == null)
            {
                return View("Value not found");
            }
            else
            {
                return PartialView("_DeleteDepartment",deleteValue);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var all = await _employeeService.GetAll();
            var result = new List<Employee>();
            foreach (var employee in all)
            {
                if (id == employee.DepartmentId)
                {
                    result.Add(employee);
                }
            }
            return View(result);
        }
        [HttpPost, ActionName("DeleteDepartment")]
        public async Task<IActionResult> DeleteDepartmentConfirmed(int id)

        {
            var department = await _service.GetById(id);
            if (department != null)
            {
                _logger.LogInformation("success");
               _service.DeleteDepartment(department);
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
                return View(department);
            }
        }

        

    }
}
