using Human_Resources.Data.Helpers;
using Human_Resources.Data.Services;
using Human_Resources.Data.Static;
using Human_Resources.Data.ViewModels;
using Human_Resources.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Human_Resources.Controllers
{
    [Authorize(Roles="Admin, HRManager")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _service;
        private readonly IPositionService _posService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public EmployeeController(IEmployeeService service, ILogger<EmployeeController> logger,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IPositionService posService)
        {
            _service = service;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _posService = posService;
        }
        [AllowAnonymous]
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
        public async Task<IActionResult> AddEmployee(EmployeeViewModel employeeViewModel,[FromForm] IFormFile PhotoURL)
        {
            
            if (!ModelState.IsValid)
            {
                var Departments = await _service.GetDepartmentdropdowns();
                var Positions = await _service.GetPositiondropdowns();
                var EducationalFields = await _service.GetEducationalFielddropdowns();
                ViewBag.Departments = new SelectList(Departments.Departments, "Id", "DepartmentName");
                ViewBag.Positions = new SelectList(Positions.Positions, "Id", "PositionName");
                ViewBag.EducationalFields = new SelectList(EducationalFields.EducationalFields, "Id", "Name");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError(error.ErrorMessage);
                }

                return View(employeeViewModel);
            }
            if (PhotoURL != null && PhotoURL.Length > 0)
            {
                var fileName = Path.GetFileName(PhotoURL.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    PhotoURL.CopyTo(stream);
                }
            }
            var pos = await _posService.GetById(employeeViewModel.PositionId);
            await _service.AddEmployee(employeeViewModel);
            var password = StringGenerator.GenerateRandomString(8);
            var username = StringGenerator.GenerateRandomString(4);
            var employeeFromEmail = await _service.GetEmployeeByEmail(employeeViewModel.Email);
            ApplicationUser user = new ApplicationUser()
            {
                Name = employeeViewModel.Name,
                Email = employeeViewModel.Email,
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = username,
                pictureURL = employeeViewModel.PhotoURL.FileName,
                PositionName = pos.PositionName,
                EmployeeId = employeeFromEmail.Id
            };
            
            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                if(employeeViewModel.Roles == Data.Enum.Roles.User)
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.User);
                }
                else if(employeeViewModel.Roles == Data.Enum.Roles.HRManager)
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.HRManager);
                    _logger.LogInformation("HRManager");
                }

                
            }
            else
            {
                _logger.LogInformation("failed to register");
            }
            _logger.LogInformation($"username: {username}, password: {password}");
            return RedirectToAction("Index", "Employee");
        }
        [HttpGet]
        public async Task<IActionResult> EditEmployee(int id)
        {
            var employee = await _service.GetById(id);
            if (employee != null)
            {
                using (var stream = new FileStream("wwwroot/images/"+employee.PhotoURL, FileMode.Open))
                {
                    // Create a new IFormFile object using the stream
                    var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(employee.PhotoURL));

                    // Use the file object as needed
                    // ...
                    var EmployeeVm = new EmployeeViewModel()
                    {
                        Id = employee.Id,
                        DepartmentId = employee.DepartmentId,
                        PositionId = employee.PositionId,
                        Sex = employee.Sex,
                        Name = employee.Name,
                        PhotoURL = file,
                        EducationalFieldId = employee.EducationalFieldId,
                        Email = employee.Email,
                        EducationalLevel = employee.EducationalLevel,
                        Roles = employee.Roles

                    };
                    var Departments = await _service.GetDepartmentdropdowns();
                    var Positions = await _service.GetPositiondropdowns();
                    var EducationalFields = await _service.GetEducationalFielddropdowns();

                    ViewBag.Departments = new SelectList(Departments.Departments, "Id", "DepartmentName");
                    ViewBag.Positions = new SelectList(Positions.Positions, "Id", "PositionName");
                    ViewBag.EducationalFields = new SelectList(EducationalFields.EducationalFields, "Id", "Name");
                    return View(EmployeeVm);
                }
            }
            return View("Not Found");
        }
        [HttpPost]
        public async Task<IActionResult> EditEmployee(EmployeeViewModel employeeViewModel, [FromForm] IFormFile PhotoURL)
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
                if (PhotoURL != null && PhotoURL.Length > 0)
                {
                    var fileName = Path.GetFileName(PhotoURL.FileName);
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        PhotoURL.CopyTo(stream);
                        _logger.LogInformation("Hi");
                    }
                }
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
                using (var stream = new FileStream("wwwroot/images/"+employee.PhotoURL, FileMode.Open))
                {
                    // Create a new IFormFile object using the stream
                    var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(employee.PhotoURL));

                    // Use the file object as needed
                    // ...
                    var EmployeeVm = new EmployeeViewModel()
                    {
                        Id = employee.Id,
                        DepartmentId = employee.DepartmentId,
                        PositionId = employee.PositionId,
                        Sex = employee.Sex,
                        Name = employee.Name,
                        PhotoURL = file,
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
            using (var stream = new FileStream("wwwroot/images/" + employee.PhotoURL, FileMode.Open))
            {
                // Create a new IFormFile object using the stream
                var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(employee.PhotoURL));

                
                var EmployeeVm = new EmployeeViewModel()
                {
                    Id = employee.Id,
                    DepartmentId = employee.DepartmentId,
                    PositionId = employee.PositionId,
                    Sex = employee.Sex,
                    Name = employee.Name,
                    PhotoURL = file,
                    EducationalFieldId = employee.EducationalFieldId,
                    Email = employee.Email,
                    EducationalLevel = employee.EducationalLevel
                };
                await _service.DeleteEmployee(EmployeeVm);
                var user = await _userManager.FindByEmailAsync(EmployeeVm.Email);
                if (user != null)
                {
                    var result = await _userManager.DeleteAsync(user);
                    if(result.Succeeded)
                    {
                        _logger.LogInformation("user delete has succeeded");
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        throw new Exception("The result didn't succeed");
                    }
                }
                else
                {
                    throw new Exception("The user isn't found");
                }
            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _service.GetById(id);
            return View(employee);
        }
        [HttpGet]
        public async Task<IActionResult> GetDropdownData()
        {
            var departments = await _service.GetDepartmentdropdowns();
            var positions = await _service.GetPositiondropdowns();
            var educationalFields = await _service.GetEducationalFielddropdowns();

            return Json(new
            {
                departments,
                positions,
                educationalFields
            });
        }
        [HttpGet]
        public async Task<IActionResult> Deactivate(int id)
        {
            var employee = await _service.GetById(id);
            if (employee != null)
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
                        DepartmentId = employee.DepartmentId,
                        PositionId = employee.PositionId,
                        Sex = employee.Sex,
                        Name = employee.Name,
                        PhotoURL = file,
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

            }
            else
            {
                return View("Not Found");
            }
        }
        [HttpPost, ActionName("Deactivate")]
        public async Task<IActionResult> DeactivateConfirmed(int id)
        {
            var employee = await _service.GetById(id);
            if (employee == null)
            {
                throw new Exception("The employee doesn't exist");

            }
            else
            {
                using (var stream = new FileStream("wwwroot/images/" + employee.PhotoURL, FileMode.Open))
                {
                    var file = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(employee.PhotoURL));
                    var empvm = new EmployeeViewModel()
                    {
                        Id = employee.Id,
                        DepartmentId = employee.DepartmentId,
                        PositionId = employee.PositionId,
                        Sex = employee.Sex,
                        Name = employee.Name,
                        PhotoURL = file,
                        EducationalFieldId = employee.EducationalFieldId,
                        Email = employee.Email,
                        EducationalLevel = employee.EducationalLevel,
                        State = Data.Enum.State.Inactive
                    };
                    await _service.UpdateEmployee(empvm);
                    var user = await _userManager.FindByEmailAsync(empvm.Email);
                    if (user != null)
                    {
                        var result = await _userManager.DeleteAsync(user);
                        if (result.Succeeded)
                        {
                            _logger.LogInformation("user delete has succeeded");
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            throw new Exception("The result didn't succeed");
                        }
                    }
                    else
                    {
                        throw new Exception("The user isn't found");
                    }
                }
            }
        }

    }
}
