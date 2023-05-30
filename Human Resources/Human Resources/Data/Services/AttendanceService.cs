using Human_Resources.Models;
using Microsoft.EntityFrameworkCore;

namespace Human_Resources.Data.Services
{
    public class AttendanceService : IAttendanceService
    { 
        private readonly AppDbContext _context;
        public AttendanceService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAttendance(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAttendance(Attendance attendance)
        {
            _context.Attendances.Remove(attendance);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Attendance>> GetAll()
        {
            var attendances = await _context.Attendances.ToListAsync();
            return attendances;
        }

        public async Task<Attendance> GetById(int id)
        {
            var attendance = await _context.Attendances.FirstOrDefaultAsync(n => n.Id == id);
            if(attendance != null)
            {
                return attendance;
            }
            else
            {
                throw new Exception("The attendance doesn't exist");
            }
        }

        public async Task UpdateAttendance(Attendance attendance)
        {
            _context.Attendances.Update(attendance);
            await _context.SaveChangesAsync();
        }
        public async Task<Attendance> GetByEmployeeId(int EmployeeId)
        {
            var attendance = await _context.Attendances.FirstOrDefaultAsync(n => n.EmployeeId == EmployeeId);
            if(attendance != null)
            {
                return attendance;
            }
            else
            {
                throw new Exception("The attendance doesn't exist");
            }
        }
    }
}
