using Human_Resources.Data;
using Human_Resources.Data.Services;
using Human_Resources.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace Human_Resources.Controllers
{
    public class LeaveController : Controller
    {
        private readonly ILeaveService leaveService;
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _accessor;
        private readonly UserManager<ApplicationUser> _userManager;
        public LeaveController(ILeaveService service , AppDbContext context, 
            IHttpContextAccessor accessor, UserManager<ApplicationUser> userManager )
        {
            leaveService = service;
            _context = context;
            _accessor = accessor;
            _userManager = userManager;

        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var leaves = await leaveService.GetAll();
            var User = _accessor.HttpContext.User;
            var user = await _userManager.GetUserAsync(User); 
            var bucket = new List<Leave>();
            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                return View(leaves);
            }
            else
            {
                foreach (var leave in leaves)
                {
                   if(leave.Employee.Position.position.PositionName == user.PositionName)
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
            var Employees = await leaveService.GetEmployeedropdowns();
            ViewBag.Employees = new SelectList(Employees.Employees, "Id", "Name");
            return View();
            
        }

        
    }
}
