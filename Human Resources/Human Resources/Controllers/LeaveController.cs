﻿using Human_Resources.Data;
using Human_Resources.Data.Services;
using Human_Resources.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Human_Resources.Data.ViewModels;

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
        public LeaveController(ILeaveService service,
            IHttpContextAccessor accessor, UserManager<ApplicationUser> userManager,
            IEmployeeService employeeService, IPositionService positionService,
            ILogger<LeaveController> logger,
            IRejectedLeaveService rejLeave, IConfirmedLeaveService confirmedLeaveService)
        {
            leaveService = service;
            _accessor = accessor;
            _userManager = userManager;
            _employeeService = employeeService;
            _positionService = positionService;
            _logger = logger;
            _rejectedLeaveService = rejLeave;
            _confirmedLeaveService = confirmedLeaveService;

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
                return View(leave);
            }
            else
            {
                await leaveService.AddLeave(leave);
                return RedirectToAction("Index");

            }
        }
        [HttpGet]
        public async Task<IActionResult> AcceptLeave(int id)
        {
            var accept = await leaveService.GetById(id);
            if (accept != null)
            {
                ConfirmedLeave confirmedLeave = new ConfirmedLeave();
                confirmedLeave.EmployeeId = accept.EmployeeId;
                confirmedLeave.Remark = accept.Remark;
                confirmedLeave.LeaveType = accept.LeaveType;

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
                    LeaveType = value.LeaveType,
                    EmployeeId = value.EmployeeId
                };

                await leaveService.DeleteLeave(leaveView);
               
                return View("index");

            }
        }
        [HttpGet]
        public async Task<IActionResult> RejectedLeave(int id)
        {
            var reject = await leaveService.GetById(id);
            if (reject != null)
            {
                RejectedLeave leave = new RejectedLeave()
                {
                    Id = reject.Id,
                    Remark = reject.Remark,
                    LeaveType = reject.LeaveType,
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
                    LeaveType = toDelete.LeaveType,
                    EmployeeId = toDelete.EmployeeId
                };
                await leaveService.DeleteLeave(reg);
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