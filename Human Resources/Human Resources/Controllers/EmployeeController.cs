using Human_Resources.Data.Services;
using Human_Resources.Data.ViewModels;
using Human_Resources.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Human_Resources.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _service;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployeeService service, ILogger<EmployeeController> logger)
        {
            _service = service;   
            _logger = logger;
        }
        public async  Task<IActionResult> Index()
        {
            var Employees = await _service.GetAll();
            return View(Employees);
        }
        public async Task<IActionResult> AddEmployee()
        {
            var lists = await _service.GetDepartmentdropdowns();
            var Positions = await _service.GetPositiondropdowns();
            var EducationalFields = await _service.GetEducationalFielddropdowns();
            
            var employeeViewModel = new EmployeeViewModel();
            ViewBag.Departments = new SelectList(lists.Departments, "Id", "DepartmentName");
            ViewBag.Positions = new SelectList(Positions.Positions, "Id", "PositionName");
            ViewBag.EducationalFields = new SelectList(EducationalFields.EducationalFields, "Id", "Name");
            return View(employeeViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeViewModel employeeViewModel)
        {
            if(!ModelState.IsValid)
            {
                var Departments = await _service.GetDepartmentdropdowns();
                var Positions = await _service.GetPositiondropdowns();

                ViewBag.Departments = new SelectList(Departments.Departments, "Id", "DepartmentName");
                ViewBag.Positions = new SelectList(Positions.Positions, "Id", "PositionName");
                return View(employeeViewModel);
            }
            await _service.AddEmployee(employeeViewModel);
            return RedirectToAction("Index", "Employee");
        }
    }
}
