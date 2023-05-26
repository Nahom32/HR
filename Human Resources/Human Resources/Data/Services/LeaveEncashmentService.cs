using Human_Resources.Models;
using Microsoft.EntityFrameworkCore;

namespace Human_Resources.Data.Services
{
    public class LeaveEncashmentService:ILeaveEncashmentService
    {
        private readonly AppDbContext _context;
        public LeaveEncashmentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddLeaveEncashment(LeaveEncashment leaveEncashment)
        {
            var encashment = await _context.LeaveEncashments.FirstOrDefaultAsync(n => n.EmployeeId == leaveEncashment.EmployeeId);
            if (encashment == null)
            {
                await _context.LeaveEncashments.AddAsync(leaveEncashment);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("leave Encashment already exists");
            }
        }

        public async Task DeleteLeaveEncashment(int id)
        {
            var encashment = await _context.LeaveEncashments.FirstOrDefaultAsync(n => n.Id == id);
            if(encashment != null)
            {
                _context.LeaveEncashments.Remove(encashment);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("leave Encashment doesn't exist");
            }
        }

        public async Task<List<LeaveEncashment>> GetAll()
        {
            var lists = await _context.LeaveEncashments.ToListAsync();
            return lists;
        }

        public async Task<LeaveEncashment> GetById(int id)
        {
            var encash = await _context.LeaveEncashments.FirstOrDefaultAsync(n => n.Id == id);
            if(encash != null)
            {
                return encash;
            }
            else
            {
                throw new Exception("Encashment doesn't exist");
            }
        }

        public async Task UpdateLeaveEncashment(LeaveEncashment leaveEncashment)
        {
            var encash = await _context.LeaveEncashments.FirstOrDefaultAsync(n => n.Id == leaveEncashment.Id);
            if (encash != null)
            {
                _context.LeaveEncashments.Update(leaveEncashment);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Encashment doesn't exist");
            }
        }
        public async Task<LeaveEncashment> GetByEmployeeId(int employeeId)
        {
            var encashment = await _context.LeaveEncashments.FirstOrDefaultAsync(n => n.EmployeeId == employeeId);
            return encashment;
        }
    }
}
