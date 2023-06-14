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
        private readonly ICheckOutTrackListService _checkOutTrackListService;
        public AttendanceController(ICheckInTrackListService checkInService, 
            IAttendanceService service, IHttpContextAccessor contextAccessor, 
            UserManager<ApplicationUser> userManager,
            ILogger<AttendanceController> logger,
            ICheckOutTrackListService checkOutTrackListService)
        {
            _checkInService = checkInService;
            _service = service;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
            _logger = logger;
            _checkOutTrackListService = checkOutTrackListService;
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
        [HttpPost]
        [Authorize(Roles = "HRManager, User")]
        public async Task<IActionResult> CheckOutPost()
        {

            var contextUser = _contextAccessor.HttpContext.User;
            var User = await _userManager.GetUserAsync(contextUser);
            var attendance = await _service.GetByEmployeeId(User.EmployeeId);
            var checkInValues = await _checkInService.GetByAttendance(attendance.Id);
            var maxVal = new DateTime(2015, 12, 25);
            var checkInGen = new CheckInTrackList();
            foreach (var checkIn in checkInValues)
            {
                if (checkIn.CheckInTime > maxVal)
                {
                    maxVal = checkIn.CheckInTime;
                    checkInGen = checkIn;
                }
            }
            if((DateTime.Now - maxVal).TotalHours <7 || DateTime.Now.Hour <= 16)
            {
                if(checkInGen.checkInStatus == Data.Enum.CheckInStatus.Late)
                {
                    attendance.NoOfLateCheck -= 1;
                    await _service.UpdateAttendance(attendance);
                }
                else if(checkInGen.checkInStatus == Data.Enum.CheckInStatus.OnTime)
                {
                    attendance.NoOnTimeCheck -= 1;
                    await _service.UpdateAttendance(attendance);
                }
            }
            await _checkOutTrackListService.AddCheckOutTrackList(new CheckOutTrackList
            {
                CheckInTrackListId = checkInGen.Id,
                CheckOutTime = DateTime.Now
            });
            return RedirectToAction("Index");

        }


          
    }
}
