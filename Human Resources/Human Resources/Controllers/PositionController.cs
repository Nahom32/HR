using Human_Resources.Data.Services;
using Human_Resources.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Human_Resources.Data.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Human_Resources.Controllers
{
    [Authorize(Roles ="Admin")]
    public class PositionController : Controller
    {
        private readonly IPositionService _service;
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<PositionController> _logger;
        public PositionController(IPositionService service, 
            ILogger<PositionController> logger,
            IEmployeeService employeeService)
        {
            _service = service;
            _logger = logger;
           _employeeService = employeeService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var allPositions = await _service.GetAll();
            var Positions = await _service.GetPositiondropdowns();
            var Departments = await _service.GetDepartmentdropdowns();
            ViewBag.Positions = new SelectList(Positions.Positions, "Id", "PositionName");
            ViewBag.Departments = new SelectList(Departments.Departments, "Id", "DepartmentName");

            return View(allPositions);
        }
        [HttpGet]
        public async Task<IActionResult> AddPosition()
        {
            var Positions = await _service.GetPositiondropdowns();
            var Departments = await _service.GetDepartmentdropdowns();
            ViewBag.Positions = new SelectList(Positions.Positions,"Id","PositionName");
            ViewBag.Departments = new SelectList(Departments.Departments, "Id", "DepartmentName");

            return PartialView("~/Views/Shared/_AddPosition.cshtml",new PositionViewModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPosition(PositionViewModel position)
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
                var Positions = await _service.GetPositiondropdowns();
                var Departments = await _service.GetDepartmentdropdowns();
                ViewBag.Positions = new SelectList(Positions.Positions, "Id", "PositionName");
                ViewBag.Departments = new SelectList(Departments.Departments, "Id", "DepartmentName");
                _logger.LogInformation("failure");
                var errorList = ModelState.Values.SelectMany(v => v.Errors)
                                          .Select(e => e.ErrorMessage)
                                          .ToList();
                var errorString = string.Join("; ", errorList);
                _logger.LogInformation(errorString);
                return PartialView("~/Views/Shared/_AddPosition.cshtml",position);
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
                var Positions = await _service.GetPositiondropdowns();
                var Departments = await _service.GetDepartmentdropdowns();
                ViewBag.Positions = new SelectList(Positions.Positions, "Id", "PositionName");
                ViewBag.Departments = new SelectList(Departments.Departments, "Id", "DepartmentName");
                PositionViewModel positionVm = new PositionViewModel()
                {
                    Id = position.Id,
                    DepartmentId = position.DepartmentId,
                    PositionName = position.PositionName,
                    PositionSalary = position.PositionSalary,
                    PositionId = position.PositionId
                };
                return PartialView("~/Views/Shared/_EditPosition.cshtml",positionVm);
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditPosition(PositionViewModel position)
        {
            if (ModelState.IsValid)
            {
                _logger.LogInformation("success1");
                await _service.UpdatePosition(position);
                return RedirectToAction("Index");
            }
            _logger.LogInformation("failure");
            var Positions = await _service.GetPositiondropdowns();
            var Departments = await _service.GetDepartmentdropdowns();
            ViewBag.Positions = new SelectList(Positions.Positions, "Id", "PositionName");
            ViewBag.Departments = new SelectList(Departments.Departments, "Id", "DepartmentName");
            return PartialView("~/Views/Shared/_EditPosition.cshtml", position);
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
                var Positions = await _service.GetPositiondropdowns();
                var Departments = await _service.GetDepartmentdropdowns();
                ViewBag.Positions = new SelectList(Positions.Positions, "Id", "PositionName");
                ViewBag.Departments = new SelectList(Departments.Departments, "Id", "DepartmentName");
                PositionViewModel positionVm = new PositionViewModel()
                {
                    Id = deleteValue.Id,
                    DepartmentId = deleteValue.DepartmentId,
                    PositionName = deleteValue.PositionName,
                    PositionSalary = deleteValue.PositionSalary,
                    PositionId = deleteValue.PositionId
                };

                return PartialView("~/Views/Shared/_EditPosition.cshtml", positionVm);
            }
        }
        [HttpPost, ActionName("DeletePosition")]
        public async Task<IActionResult> DeletePositionConfirmed(int id)
        {
            var position = await _service.GetById(id);
            var unassigned = await _service.GetPositionByName("Unassigned");
            if (position != null)
            {
                PositionViewModel positionVm = new PositionViewModel()
                {
                    Id = position.Id,
                    DepartmentId = position.DepartmentId,
                    PositionName = position.PositionName,
                    PositionSalary = position.PositionSalary,
                    PositionId = position.PositionId
                };
                var employees = await _service.GetEmployeeByPosition(id);
                if (employees != null)
                {
                    foreach (var employee in employees)
                    {
                        using (var stream = new FileStream("wwwroot/images/" + employee.PhotoURL, FileMode.Open))
                        {
                            // Create a new IFormFile object using the stream
                            var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(employee.PhotoURL));

                            // Use the file object as needed
                            // ...
                            var EmployeeVm = new EmployeeViewModel()
                            {
                                Id = employee.Id,
                                //DepartmentId = employee.DepartmentId,
                                PositionId = unassigned.Id,
                                Sex = employee.Sex,
                                Name = employee.Name,
                                PhotoURL = file,
                                EducationalFieldId = employee.EducationalFieldId,
                                Email = employee.Email,
                                EducationalLevel = employee.EducationalLevel,
                                Roles = employee.Roles
                            };
                           await _employeeService.UpdateEmployee(EmployeeVm);
                        }
                    }
                }
                await _service.DeletePosition(positionVm);
                return RedirectToAction("Index");
            }
            else
            {
                var Positions = await _service.GetPositiondropdowns();
                var Departments = await _service.GetDepartmentdropdowns();
                ViewBag.Positions = new SelectList(Positions.Positions, "Id", "PositionName");
                ViewBag.Departments = new SelectList(Departments.Departments, "Id", "DepartmentName");
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
