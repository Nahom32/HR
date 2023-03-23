using Human_Resources.Data.Services;
using Human_Resources.Data.ViewModels;
using Human_Resources.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Human_Resources.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _service;
        private readonly ILogger<EmployeeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public EmployeeController(IEmployeeService service, ILogger<EmployeeController> logger,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _service = service;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
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
            if (!ModelState.IsValid)
            {
                var Departments = await _service.GetDepartmentdropdowns();
                var Positions = await _service.GetPositiondropdowns();
                var EducationalFields = await _service.GetEducationalFielddropdowns();

                ViewBag.Departments = new SelectList(Departments.Departments, "Id", "DepartmentName");
                ViewBag.Positions = new SelectList(Positions.Positions, "Id", "PositionName");
                ViewBag.EducationalFields = new SelectList(EducationalFields.EducationalFields, "Id", "Name");
                return View(employeeViewModel);
            }
            await _service.AddEmployee(employeeViewModel);
            return RedirectToAction("Index", "Employee");
        }
        [HttpGet]
        public async Task<IActionResult> EditEmployee(int id)
        {
            var employee = await _service.GetById(id);
            if (employee != null)
            {
                var EmployeeVm = new EmployeeViewModel()
                {
                    Id = employee.Id,
                    DepartmentId = employee.DepartmentId,
                    PositionId = employee.PositionId,
                    Sex = employee.Sex,
                    Name = employee.Name,
                    PhotoURL = employee.PhotoURL,
                    EducationalFieldId = employee.EducationalFieldId,
                    Email = employee.Email,
                    EducationalLevel = employee.EducationalLevel

                };
                var Departments = await _service.GetDepartmentdropdowns();
                var Positions = await _service.GetPositiondropdowns();
                var EducationalFields = await _service.GetEducationalFielddropdowns();

                ViewBag.Departments = new SelectList(Departments.Departments, "Id", "DepartmentName");
                ViewBag.Positions = new SelectList(Positions.Positions, "Id", "PositionName");
                ViewBag.EducationalFields = new SelectList(EducationalFields.EducationalFields, "Id", "Name");
                return View(EmployeeVm);
            }
            return View("Not Found");
        }
        [HttpPost]
        public async Task<IActionResult> EditEmployee(EmployeeViewModel employeeViewModel)
        {
            if (!ModelState.IsValid)
            {
                var Departments = await _service.GetDepartmentdropdowns();
                var Positions = await _service.GetPositiondropdowns();
                var EducationalFields = await _service.GetEducationalFielddropdowns();

                ViewBag.Departments = new SelectList(Departments.Departments, "Id", "DepartmentName");
                ViewBag.Positions = new SelectList(Positions.Positions, "Id", "PositionName");
                ViewBag.EducationalFields = new SelectList(EducationalFields.EducationalFields, "Id", "Name");
                return View(employeeViewModel);

            }
            else
            {
                await _service.UpdateEmployee(employeeViewModel);
                return RedirectToAction("Index");
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var employee = await _service.GetById(id);
            if (employee != null)
            {
                var EmployeeVm = new EmployeeViewModel()
                {
                    Id = employee.Id,
                    DepartmentId = employee.DepartmentId,
                    PositionId = employee.PositionId,
                    Sex = employee.Sex,
                    Name = employee.Name,
                    PhotoURL = employee.PhotoURL,
                    EducationalFieldId = employee.EducationalFieldId,
                    Email = employee.Email,
                    EducationalLevel = employee.EducationalLevel

                };
                var Departments = await _service.GetDepartmentdropdowns();
                var Positions = await _service.GetPositiondropdowns();
                var EducationalFields = await _service.GetEducationalFielddropdowns();

                ViewBag.Departments = new SelectList(Departments.Departments, "Id", "DepartmentName");
                ViewBag.Positions = new SelectList(Positions.Positions, "Id", "PositionName");
                ViewBag.EducationalFields = new SelectList(EducationalFields.EducationalFields, "Id", "Name");
                return View(EmployeeVm);
            }
            else
            {
                return View("Not Found");
            }

        }
        [HttpPost, ActionName("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployeeConfirmed(int id)
        {
            var employee = await _service.GetById(id);
            var EmployeeVm = new EmployeeViewModel()
            {
                Id = employee.Id,
                DepartmentId = employee.DepartmentId,
                PositionId = employee.PositionId,
                Sex = employee.Sex,
                Name = employee.Name,
                PhotoURL = employee.PhotoURL,
                EducationalFieldId = employee.EducationalFieldId,
                Email = employee.Email,
                EducationalLevel = employee.EducationalLevel
            };
            await _service.DeleteEmployee(EmployeeVm);
            return RedirectToAction("Index");
        }
    }
}
