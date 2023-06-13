using Human_Resources.Data.Services;
using Human_Resources.Data.ViewModels;
using Human_Resources.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Human_Resources.Controllers
{
    [Authorize]
    public class AttendanceController : Controller
    {
        private readonly IAttendanceService _service;
        private readonly ICheckInTrackListService _checkInService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AttendanceController> _logger;
        public AttendanceController(ICheckInTrackListService checkInService, 
            IAttendanceService service, IHttpContextAccessor contextAccessor, 
            UserManager<ApplicationUser> userManager,
            ILogger<AttendanceController> logger)
        {
            _checkInService = checkInService;
            _service = service;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _logger = logger;
        }
        [HttpGet]
        
        public async Task<IActionResult> Index()
        {
            var contextUser = _contextAccessor.HttpContext.User;
            var User = await _userManager.GetUserAsync(contextUser);
            var attendance = await _service.GetByEmployeeId(User.EmployeeId);
            var checkInValues = await _checkInService.GetByAttendance(attendance.Id);
            AttendanceViewModel attendanceVm = new AttendanceViewModel();
            
            attendanceVm.Attendance = attendance;
            attendanceVm.CheckInTracks = checkInValues;
            var maxVal = new DateTime(2015, 12, 25);
            foreach(var checkIn in attendanceVm.CheckInTracks)
            {
                if(checkIn.AttendanceId == attendanceVm.Attendance.Id && checkIn.CheckInTime > maxVal)
                {
                    maxVal = checkIn.CheckInTime;
                }
            }
            attendanceVm.LastTime = maxVal;
            return View(attendanceVm);
        }
        [HttpPost, ActionName("Index")]
        public async Task<IActionResult> IndexPost()
        {
            var contextUser = _contextAccessor.HttpContext.User;
            var User = await _userManager.GetUserAsync(contextUser);
            var attendance = await _service.GetByEmployeeId(User.EmployeeId);
            var checkInValues = await _checkInService.GetByAttendance(attendance.Id);
            AttendanceViewModel attendanceVm = new AttendanceViewModel();
            attendanceVm.Attendance = attendance;
            attendanceVm.CheckInTracks = checkInValues;

            DateTime now = DateTime.Now;
                string amPm = now.ToString("tt");
                if (amPm == "AM" && now.Hour < 9)
                {
                    attendanceVm.Attendance.NoOnTimeCheck += 1;
                    await _service.UpdateAttendance(attendanceVm.Attendance);
                    CheckInTrackList checkInTrackList = new CheckInTrackList();
                    checkInTrackList.AttendanceId = attendanceVm.Attendance.Id;
                    checkInTrackList.CheckInTime = DateTime.Now;
                    checkInTrackList.checkInStatus = Data.Enum.CheckInStatus.OnTime;
                    await _checkInService.AddCheckInTrackList(checkInTrackList);
                    return RedirectToAction("Index");
                }
                else if (amPm == "AM" && now.Hour < 10)
                {
                    attendanceVm.Attendance.NoOfLateCheck += 1;
                    await _service.UpdateAttendance(attendanceVm.Attendance);
                    CheckInTrackList checkInTrackList = new CheckInTrackList();
                    checkInTrackList.AttendanceId = attendanceVm.Attendance.Id;
                    checkInTrackList.CheckInTime = DateTime.Now;
                    checkInTrackList.checkInStatus = Data.Enum.CheckInStatus.Late;
                    await _checkInService.AddCheckInTrackList(checkInTrackList);
                    return RedirectToAction("Index");

                }
                else
                {
                    attendanceVm.Attendance.NoOfAbsentCheck += 1;
                    await _service.UpdateAttendance(attendanceVm.Attendance);
                    CheckInTrackList checkInTrackList = new CheckInTrackList();
                    checkInTrackList.AttendanceId = attendanceVm.Attendance.Id;
                    checkInTrackList.CheckInTime = DateTime.Now;
                    checkInTrackList.checkInStatus = Data.Enum.CheckInStatus.Absent;
                    await _checkInService.AddCheckInTrackList(checkInTrackList);
                    return RedirectToAction("Index");

                }
            
        }
        [HttpGet]
        [Authorize(Roles ="Admin,HRManager")]
        public async Task<IActionResult> EmployeeStatus(int EmployeeId)
        {
            var attendance = await _service.GetByEmployeeId(EmployeeId);
            var checkInValues = await _checkInService.GetByAttendance(attendance.Id);
            AttendanceViewModel attendanceVm = new AttendanceViewModel();

            attendanceVm.Attendance = attendance;
            attendanceVm.CheckInTracks = checkInValues;
            var maxVal = new DateTime(2015, 12, 25);
            foreach (var checkIn in attendanceVm.CheckInTracks)
            {
                if (checkIn.AttendanceId == attendanceVm.Attendance.Id && checkIn.CheckInTime > maxVal)
                {
                    maxVal = checkIn.CheckInTime;
                }
            }
            attendanceVm.LastTime = maxVal;
            return View(attendanceVm);

        }

          
    }
}
