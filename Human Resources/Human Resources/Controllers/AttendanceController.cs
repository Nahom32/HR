using Human_Resources.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Human_Resources.Controllers
{
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService _service;
        private readonly ICheckInTrackListService _checkInService;
        public AttendanceController(ICheckInTrackListService checkInService, IAttendanceService service)
        {
            _checkInService = checkInService;
            _service = service;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

    }
}
