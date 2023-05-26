using Human_Resources.Data;
using Human_Resources.Data.Services;
using Human_Resources.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Human_Resources.Data.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace Human_Resources.Controllers
{
    
    public class LeaveController : Controller
    {
        private readonly ILeaveService leaveService;
        private readonly IHttpContextAccessor _accessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmployeeService _employeeService;
        private readonly IPositionService _positionService;
        private readonly ILogger<LeaveController> _logger;
        private readonly IRejectedLeaveService _rejectedLeaveService;
        private readonly IConfirmedLeaveService _confirmedLeaveService;
        private readonly ILeaveEncashmentService _encashment;
        private readonly ILeaveTypeService _leaveTypeService;
        public LeaveController(ILeaveService service,
            IHttpContextAccessor accessor, UserManager<ApplicationUser> userManager,
            IEmployeeService employeeService, IPositionService positionService,
            ILogger<LeaveController> logger,
            IRejectedLeaveService rejLeave, 
            IConfirmedLeaveService confirmedLeaveService,
            ILeaveEncashmentService encashment,
            ILeaveTypeService leaveTypeService)
        {
            leaveService = service;
            _accessor = accessor;
            _userManager = userManager;
            _employeeService = employeeService;
            _positionService = positionService;
            _logger = logger;
            _rejectedLeaveService = rejLeave;
            _confirmedLeaveService = confirmedLeaveService;
            _encashment = encashment;
            _leaveTypeService = leaveTypeService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var leaves = await leaveService.GetAll();
            var User = _accessor.HttpContext.User;
            var user = await _userManager.GetUserAsync(User);
            var bucket = new List<Leave>();
            if(leaves == null)
            {
                leaves = new List<Leave>();
            }

            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {

                return View(leaves);
            }
            else
            {
                foreach (var leave in leaves)
                {
                    var pos = await _positionService.GetById(leave.Employee.PositionId);
                    if(pos.position == null)
                    {
                        continue;
                    }else if(pos.position.PositionName == user.PositionName) // Check for parent node
                    {
                        bucket.Add(leave);
                    }

                }
                return View(bucket);
            }

        }
        [HttpGet]
        public async Task<IActionResult> RequestLeave()
        {
            var User = _accessor.HttpContext.User;
            var leaveDropDowns = await leaveService.GetLeaveTypedropdowns();
            ViewBag.leaveTypeDropDowns = new SelectList(leaveDropDowns.LeaveTypes, "Id", "LeaveName");
            var user = await _userManager.GetUserAsync(User);
            if(user != null)
            {
                var employee = await _employeeService.GetById(user.EmployeeId);
                LeaveViewModel leave = new LeaveViewModel();
                leave.EmployeeId = employee.Id;
                return View(leave);
            }
            else
            {
                throw new Exception("the user doesn't exist");
            }   
        }
        [HttpPost]
        public async Task<IActionResult> RequestLeave(LeaveViewModel leave)
        {
            if (!ModelState.IsValid)
            {
                var leaveDropDowns = await leaveService.GetLeaveTypedropdowns();
                ViewBag.leaveTypeDropDowns = new SelectList(leaveDropDowns.LeaveTypes, "Id", "LeaveName");
                return View(leave);
            }
            else
            {
                var searchLeave = await _leaveTypeService.GetById(leave.LeaveTypesId);
                var encashment = await _encashment.GetByEmployeeId(leave.EmployeeId);

                if(searchLeave.Days <= encashment.Credit )
                {
                    await leaveService.AddLeave(leave);
                    encashment.Credit = encashment.Credit - searchLeave.Days;
                    await _encashment.UpdateLeaveEncashment(encashment);
                    return RedirectToAction("Index");
                }
                else
                {

                    return RedirectToAction("Error");
                }
                

            }
        }
        [HttpGet]
        public async Task<IActionResult> AcceptLeave(int id)
        {
            var accept = await leaveService.GetById(id);
            var leaveDropDowns = await leaveService.GetLeaveTypedropdowns();
            ViewBag.leaveTypeDropDowns = new SelectList(leaveDropDowns.LeaveTypes, "Id", "LeaveName");
            if (accept != null)
            {
                ConfirmedLeave confirmedLeave = new ConfirmedLeave();
                confirmedLeave.EmployeeId = accept.EmployeeId;
                confirmedLeave.Remark = accept.Remark;
                confirmedLeave.LeaveTypesId = accept.LeaveTypesId;
                return View(confirmedLeave);
            }
            else
            {
                return View("The leave doesn't exist");
            }

        }
        [HttpPost]
        public async Task<IActionResult> AcceptLeave(ConfirmedLeave confirmedLeave)
        {
            confirmedLeave.Id = 0;
            if (!ModelState.IsValid)
            {
                var leaveDropDowns = await leaveService.GetLeaveTypedropdowns();
                ViewBag.leaveTypeDropDowns = new SelectList(leaveDropDowns.LeaveTypes, "Id", "LeaveName");
                _logger.LogInformation("leave unsuccessful");
                return View(confirmedLeave);
            }
            else
            {
                await _confirmedLeaveService.AddConfirmedLeave(confirmedLeave);
                var value = await leaveService.SearchByEmployeeId(confirmedLeave.EmployeeId);
                LeaveViewModel leaveView = new LeaveViewModel()
                {
                    Id = value.Id,
                    Remark = value.Remark,
                    LeaveTypesId = value.LeaveTypesId,
                    EmployeeId = value.EmployeeId,
                    LeaveStatus = Data.Enum.LeaveStatus.Accepted
                };

                await leaveService.UpdateLeave(leaveView);
                var encashment = await _encashment.GetByEmployeeId(leaveView.EmployeeId);
                var searchLeave = await _leaveTypeService.GetById(leaveView.LeaveTypesId); 
                encashment.Credit = encashment.Credit - searchLeave.Days;
                await _encashment.UpdateLeaveEncashment(encashment);

                return View("index");

            }
        }
        [HttpGet]
        public async Task<IActionResult> RejectedLeave(int id)
        {
            var reject = await leaveService.GetById(id);
            var leaveDropDowns = await leaveService.GetLeaveTypedropdowns();
            ViewBag.leaveTypeDropDowns = new SelectList(leaveDropDowns.LeaveTypes, "Id", "LeaveName");
            if (reject != null)
            {
                RejectedLeave leave = new RejectedLeave()
                {
                    Id = reject.Id,
                    Remark = reject.Remark,
                    LeaveTypesId = reject.LeaveTypesId,
                    EmployeeId = reject.EmployeeId
                };
                return View(leave);
            }
            else
            {
                return View("The leave doesn't exist");
            }
        }
        [HttpPost]
        public async Task<IActionResult> RejectedLeave(RejectedLeave leave)
        {
            leave.Id = 0;
            if(!ModelState.IsValid)
            {
                var leaveDropDowns = await leaveService.GetLeaveTypedropdowns();
                ViewBag.leaveTypeDropDowns = new SelectList(leaveDropDowns.LeaveTypes, "Id", "LeaveName");

                return View(leave);
            }
            else
            {
                await _rejectedLeaveService.AddRejectedLeave(leave);
                var toDelete = await leaveService.SearchByEmployeeId(leave.EmployeeId);
                LeaveViewModel reg = new LeaveViewModel()
                {
                    Id = toDelete.Id,
                    Remark = toDelete.Remark,
                    LeaveTypesId = toDelete.LeaveTypesId,
                    EmployeeId = toDelete.EmployeeId,
                    LeaveStatus = Data.Enum.LeaveStatus.Rejected
                };
                await leaveService.UpdateLeave(reg);
                return RedirectToAction("index");

            }
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var Tab = await leaveService.GetById(id);
            if (Tab != null)
            {
                return PartialView("_LeaveDetails.cshtml",Tab);
            }
            else
            {
                throw new Exception("The Leave doesn't exist");

            }
        }
        

    }
}
