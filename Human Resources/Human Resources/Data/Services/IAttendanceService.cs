using Human_Resources.Data.ViewModels;
using Human_Resources.Models;

namespace Human_Resources.Data.Services
{
    public interface IAttendanceService
    {
        public Task<List<Attendance>> GetAll();
        public Task<Attendance> GetById(int id);
        public Task AddAttendance(Attendance attendance);
        public Task UpdateAttendance(Attendance attendance);
        public Task DeleteAttendance(Attendance attendance);
        public Task<Attendance> GetByEmployeeId(int EmployeeId);
    }
}
