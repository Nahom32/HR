using ClosedXML.Excel;
using Human_Resources.Data;
using Human_Resources.Data.Helpers;
using Human_Resources.Data.Services;
using Human_Resources.Data.Static;
using Human_Resources.Data.ViewModels;
using Human_Resources.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using X.PagedList;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Human_Resources.Controllers
{
    [Authorize(Roles = "Admin,HRManager")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _service;
        private readonly IPositionService _posService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;
        private readonly IEmailService _email;
        private readonly IAppraisalService _appraisal;
        private readonly ILeaveEncashmentService _encashment;
        private readonly IAttendanceService _attendanceService;
        private readonly ICheckInTrackListService _checkInTrackListService;
        private readonly IRewardService _rewardService;
        public EmployeeController(IEmployeeService service, ILogger<EmployeeController> logger,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IPositionService posService, AppDbContext context, IEmailService email, 
            IAppraisalService appraisal, ILeaveEncashmentService encashmentService, 
            IAttendanceService attendanceService, ICheckInTrackListService checkInTrackListService,
            IRewardService rewardService)
        {
            _service = service;
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
            _posService = posService;
            _context = context;
            _email = email;
            _appraisal = appraisal;
            _encashment = encashmentService;
            _attendanceService = attendanceService;
            _checkInTrackListService = checkInTrackListService;
            _rewardService = rewardService;
        }
        //[HttpGet]
        //public async Task<IActionResult> Index(int? page = 1)
        //{
        //    var PageSize = 10;
        //    var Employees = await _service.GetAll();
        //    var Filter = new List<Employee>();
        //    foreach (var i in Employees)
        //    {
        //        if (i.State == Data.Enum.State.Active)
        //        {
        //            Filter.Add(i);
        //        }
        //    }
        //    var File = await Filter.ToPagedListAsync(page ?? 1, PageSize);
        //    var filterVm = new FilterVM();
        //    filterVm.Employees = File;
        //    return View(filterVm);
        //}
        [HttpGet]
        public IActionResult Index()
        {
            //var employeeViewModel = new EmployeeTableViewModel();
            //var request = new DataTableRequest { Start = 0, Length = 10 };
            //employeeViewModel.Employees = await _service.SearchEmployees(request);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(FilterVM filterVm)
        {


            var PageSize = 10;
            var Employees = await _service.GetAll();
            var Filter = new List<Employee>();
            foreach (var i in Employees)
            {
                if (i.State == filterVm.State)
                {
                    Filter.Add(i);
                }
            }
            var Paged = await Filter.ToPagedListAsync(filterVm?.Page ?? 1, PageSize);
            filterVm.Employees = Paged;
            return View(filterVm);

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
        public async Task<IActionResult> AddEmployee(EmployeeViewModel employeeViewModel, [FromForm] IFormFile PhotoURL)
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
            var fuser = await _userManager.FindByNameAsync(user.UserName);
            if (fuser == null)
            {

                var result = await _userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    if (employeeViewModel.Roles == Data.Enum.Roles.User)
                    {
                        await _userManager.AddToRoleAsync(user, UserRoles.User);
                    }
                    else if (employeeViewModel.Roles == Data.Enum.Roles.HRManager)
                    {
                        await _userManager.AddToRoleAsync(user, UserRoles.HRManager);
                        _logger.LogInformation("HRManager");
                    }

                }
                else
                {
                    throw new Exception("Failed to Register");
                }
                TempData["username"] = username;
                TempData["password"] = password;
                _logger.LogInformation($"username: {username}, password: {password}");
                var mail = new EMessage(new string[] { employeeViewModel.Email },"Personal Information",$"username: {username}, password: {password}");
                
                await _email.SendEmailAsync(mail);
                await _encashment.AddLeaveEncashment(new LeaveEncashment()
                {
                    EmployeeId = employeeFromEmail.Id,
                    Credit = 50
                });
                await _attendanceService.AddAttendance(new Attendance()
                {
                    EmployeeId = employeeFromEmail.Id
                });
                
                return RedirectToAction("Index");
            }
            else
            {
                throw new Exception("the user exists");
            }

        }
        [HttpGet]
        public async Task<IActionResult> EditEmployee(int id)
        {
            var employee = await _service.GetById(id);
            if (employee != null)
            {
                using (var stream = new FileStream("wwwroot/images/" + employee.PhotoURL, FileMode.Open))
                {
                    // Create a new IFormFile object using the stream
                    var file = new FormFile(stream, 0, stream.Length, null,  Path.GetFileName(employee.PhotoURL));

                    // Use the file object as needed
                    // ...
                    var EmployeeVm = new EmployeeViewModel()
                    {
                        Id = employee.Id,
                        //DepartmentId = employee.DepartmentId,
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
                using (var stream = new FileStream("wwwroot/images/" + employee.PhotoURL, FileMode.Open))
                {
                    // Create a new IFormFile object using the stream
                    var file = new FormFile(stream, 0, stream.Length, null, "wwwroot/images/"+ Path.GetFileName(employee.PhotoURL));

                    // Use the file object as needed
                    // ...
                    var EmployeeVm = new EmployeeViewModel()
                    {
                        Id = employee.Id,
                        //DepartmentId = employee.DepartmentId,
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
                    //DepartmentId = employee.DepartmentId,
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

        [HttpGet]
        [AllowAnonymous]
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
                        //DepartmentId = employee.DepartmentId,
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
                       // DepartmentId = employee.DepartmentId,
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
        [HttpPost]
        public async Task<IActionResult> ListInactive()
        {
            var Employees = await _service.getEmployees();
            var InactiveEmployees = new List<Employee>();
            foreach(var i in Employees)
            {
                if(i.State == Data.Enum.State.Inactive)
                {
                    InactiveEmployees.Add(i);
                }
            }
            return View(InactiveEmployees);
        }
        [HttpPost]
        [AllowAnonymous]
        public async  Task<IActionResult> GetEmployees()
        {
            var draw = Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault()?.Trim();
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            DataTableRequest dataTable = new DataTableRequest();
            dataTable.SearchValue = searchValue;
            if (Int32.TryParse(start, out int startValue))
            {
                dataTable.Start = startValue;
            }
            if (Int32.TryParse(length, out int lengthValue))
            {
                dataTable.Length = lengthValue;
            }
            //if (Int32.TryParse(sortColumn, out int sortColumnValue))
            //{
            //    dataTable.SortColumn = sortColumnValue;
            //}
            dataTable.SortDirection = "asc";
            if (Int32.TryParse(draw, out int drawValue))
            {
                dataTable.Draw = drawValue;
            }

            var result = await _service.SearchEmployees(dataTable);
            recordsTotal = result.Count();
            
            return Json( new { draw, recordsFiltered = result.Count, recordsTotal = recordsTotal, data = result,
                sortColumn = sortColumn, sortColumnDirection = sortColumnDirection});
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetGrades(int? id)
        {
            var grades = new List<Appraisal>();
            var total = await _appraisal.GetAll();
            foreach ( var item in total )
            {
                if(item.EmployeeId == id)
                {
                    grades.Add(item);
                }
            }
            return View(grades);
            
        }
        [HttpGet]
        public async Task<IActionResult> InitEncash()
        {
            var All = await _service.GetAll();
            foreach ( var item in All )
            {
                await _encashment.AddLeaveEncashment(new LeaveEncashment()
                {
                    EmployeeId = item.Id,
                    Credit = 50
                });

            }
            return RedirectToAction("success");

        }
        [HttpGet]
        public  JsonResult GetPositionsByDepartment(int DepartmentId)
        {
            var value = _service.GetPositionsByDepartment(DepartmentId);
            return Json(value);
        }
        public async Task<IActionResult> ExcelExport()
        {
            var employees = await _service.FilterEmployeeState();

            using(var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("employees");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Name";
                worksheet.Cell(currentRow, 2).Value = "Department";
                worksheet.Cell(currentRow, 3).Value = "Position";
                worksheet.Cell(currentRow, 4).Value = "Initial Salary(birr)";
                worksheet.Cell(currentRow, 5).Value = "Final Salary(birr)";
                worksheet.Column("A").Width = 20;
                worksheet.Column("B").Width = 20;
                worksheet.Column("C").Width = 20;
                worksheet.Column("D").Width = 20;
                worksheet.Column("E").Width = 20;
                foreach(var employee in employees)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = employee.Name;
                    worksheet.Cell(currentRow, 2).Value = employee.Position.Department.DepartmentName;
                    worksheet.Cell(currentRow, 3).Value = employee.Position.PositionName;
                    worksheet.Cell(currentRow, 4).Value = employee.Position.PositionSalary;
                    var attendanceStatistics = await _attendanceService.GetByEmployeeId(employee.Id);
                    worksheet.Cell(currentRow, 5).Value = employee.Position.PositionSalary - (0.01 * attendanceStatistics.NoOfLateCheck * employee.Position.PositionSalary) - (0.05 * attendanceStatistics.NoOfAbsentCheck * employee.Position.PositionSalary);

                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(
                        content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "employees.xlsx");
                }



            }
            

        }



    }
}
