using Human_Resources.Data.Services;
using Human_Resources.Data.ViewModels;
using Human_Resources.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Human_Resources.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PromotionController : Controller
    {
        private readonly IPromotionService _service;
        private readonly IEmployeeService _empService;
        private readonly ILogger<PromotionController> _logger;
        public PromotionController(IPromotionService service, IEmployeeService empService, ILogger<PromotionController> logger)
        {
            _service = service;
            _empService = empService;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var lists = await _service.GetAll();
            return View(lists);
        }
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPromotion()
        {
            PromotionViewModel promotionVM = new PromotionViewModel();
            var Positiondropdowns = await _service.GetPositiondropdowns();
            var Employeedropdowns = await _service.GetEmployeedropdowns();
            ViewBag.PositionFrom = new SelectList(Positiondropdowns.Positions, "Id", "PositionName");
            ViewBag.PositionTo = new SelectList(Positiondropdowns.Positions, "Id", "PositionName");
            ViewBag.Employeedropdowns = new SelectList(Employeedropdowns.Employees, "Id", "Name");
            return View(promotionVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPromotion(PromotionViewModel promotionVM)
        {
            if (!ModelState.IsValid)
            {
                var Positiondropdowns = await _service.GetPositiondropdowns();
                var Employeedropdowns = await _service.GetEmployeedropdowns();
                ViewBag.PositionFrom = new SelectList(Positiondropdowns.Positions, "Id", "PositionName");
                ViewBag.PositionTo = new SelectList(Positiondropdowns.Positions, "Id", "PositionName");
                ViewBag.Employeedropdowns = new SelectList(Employeedropdowns.Employees, "Id", "Name");
                _logger.LogInformation("Failed");
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError(error.ErrorMessage);
                }

                return View(promotionVM);
            }
            var employee = await _empService.GetById(promotionVM.EmployeeId);
            if (employee != null)
            {
                employee.PositionId = promotionVM.toPositionId;
                using (var stream = new FileStream("wwwroot/images/" + employee.PhotoURL, FileMode.Open))
                {
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

                    var Positions = await _service.GetPositiondropdowns();


                    ViewBag.Positions = new SelectList(Positions.Positions, "Id", "PositionName");
                    await _empService.UpdateEmployee(EmployeeVm);

                }
                await _service.AddPromotion(promotionVM);
                return RedirectToAction("Index", "Promotion");

            }
            else
            {
                return View("The selected Employee doesn't exist");

            }
        }

        [HttpGet]
        public async Task<IActionResult> EditPromotion(int id)
        {
            var promotion = await _service.GetById(id);
            if (promotion != null)
            {
                PromotionViewModel promotionVm = new PromotionViewModel()
                {
                    Id = promotion.Id,
                    Reason = promotion.Reason,
                    fromPositionId = promotion.fromPositionId,
                    toPositionId = promotion.toPositionId,
                    PositionChange = promotion.PositionChange,
                    EmployeeId = promotion.EmployeeId,

                };
                var Positiondropdowns = await _service.GetPositiondropdowns();
                var Employeedropdowns = await _service.GetEmployeedropdowns();
                ViewBag.PositionFrom = new SelectList(Positiondropdowns.Positions, "Id", "PositionName");
                ViewBag.PositionTo = new SelectList(Positiondropdowns.Positions, "Id", "PositionName");
                ViewBag.Employeedropdowns = new SelectList(Employeedropdowns.Employees, "Id", "Name");
                return View(promotionVm);
            }
            else
            {
                return View("This Promotion haven't been made");
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditPromotion(PromotionViewModel promotionVm)
        {
            if (!ModelState.IsValid)
            {
                var Positiondropdowns = await _service.GetPositiondropdowns();
                var Employeedropdowns = await _service.GetEmployeedropdowns();
                ViewBag.PositionFrom = new SelectList(Positiondropdowns.Positions, "Id", "PositionName");
                ViewBag.PositionTo = new SelectList(Positiondropdowns.Positions, "Id", "PositionName");
                ViewBag.Employeedropdowns = new SelectList(Employeedropdowns.Employees, "Id", "Name",promotionVm.EmployeeId);
                return View(promotionVm);
            }
            else
            {
                _logger.LogInformation($"The number is {promotionVm.EmployeeId}");
                var employee = await _empService.GetById(promotionVm.EmployeeId);
                if (employee != null)
                {
                    employee.PositionId = promotionVm.toPositionId;
                    using (var stream = new FileStream("wwwroot/images/" + employee.PhotoURL, FileMode.Open))
                    {
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

                        var Positions = await _service.GetPositiondropdowns();


                        ViewBag.Positions = new SelectList(Positions.Positions, "Id", "PositionName");
                        await _empService.UpdateEmployee(EmployeeVm);

                    }
                    await _service.UpdatePromotion(promotionVm);
                    return RedirectToAction("Index", "Promotion");
                }
                else
                {
                    return View("The employee Returned doesn't exist");
                }
            }

        }
        public async Task<IActionResult> DeletePromotion(int id)
        {
            var promotion = await _service.GetById(id);
            if (promotion != null)
            {
                PromotionViewModel promotionVm = new PromotionViewModel()
                {
                    Id = promotion.Id,
                    Reason = promotion.Reason,
                    fromPositionId = promotion.fromPositionId,
                    toPositionId = promotion.toPositionId,
                    PositionChange = promotion.PositionChange,
                    EmployeeId = promotion.EmployeeId,
                };
                var Positiondropdowns = await _service.GetPositiondropdowns();
                var Employeedropdowns = await _service.GetEmployeedropdowns();
                ViewBag.PositionFrom = new SelectList(Positiondropdowns.Positions, "Id", "PositionName");
                ViewBag.PositionTo = new SelectList(Positiondropdowns.Positions, "Id", "PositionName");
                ViewBag.Employeedropdowns = new SelectList(Employeedropdowns.Employees, "Id", "Name",promotion.EmployeeId);
                return View(promotionVm);
            }
            else
            {
                return View("Such a view doesn't exist");

            }
        }
        [HttpPost, ActionName("DeletePromotion")]
        public async Task<IActionResult> DeletePromotionConfirmed(int id)
        {
            var promotion = await _service.GetById(id);
            if (promotion != null)
            {
                var PromotionVm = new PromotionViewModel()
                {
                    Id = promotion.Id,
                    PositionChange = promotion.PositionChange,
                    EmployeeId = promotion.EmployeeId,
                    Reason = promotion.Reason,
                    fromPositionId = promotion.fromPositionId,
                    toPositionId = promotion.toPositionId

                };
                var employee = await _empService.GetById(PromotionVm.EmployeeId);
                if (employee != null)
                {
                    employee.PositionId = PromotionVm.fromPositionId;
                    using (var stream = new FileStream("wwwroot/images/" + employee.PhotoURL, FileMode.Open))
                    {
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

                        var Positions = await _service.GetPositiondropdowns();


                        ViewBag.Positions = new SelectList(Positions.Positions, "Id", "PositionName");
                        await _empService.UpdateEmployee(EmployeeVm);

                    }
                    await _service.DeletePromotion(PromotionVm);
                    return RedirectToAction("Index", "Promotion");
                }
                else
                {
                    return View("The object doesn't exist");
                }


            }
            else
            {
                return View("The object doesn't exist");
            }
        }
    }
}
