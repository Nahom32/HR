using Human_Resources.Models;
using Microsoft.EntityFrameworkCore;

namespace Human_Resources.Data.Services
{
    public class LeaveTypeService : ILeaveTypeService
    {
        private readonly AppDbContext _context;
        public LeaveTypeService(AppDbContext context)
        {
            _context = context;
        }

        public async Task  AddLeaveType(LeaveTypes leaveType)
        {
            var leave = await _context.LeaveType.FirstOrDefaultAsync(n => n.LeaveName == leaveType.LeaveName);
            if(leave == null)
            {
               await _context.LeaveType.AddAsync(leaveType);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("The Leave Already Exists");
            }
        }

        public async Task DeleteLeaveType(int id)
        {
            var leave = await _context.LeaveType.FirstOrDefaultAsync(n => n.Id == id);
            if(leave != null)
            {
                _context.LeaveType.Remove(leave);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("The leavetype doesn't exist");
            }
        }

        public async Task<List<LeaveTypes>> GetAll()
        {
            var list = await _context.LeaveType.ToListAsync();
            return list;
        }

        public async Task<LeaveTypes> GetById(int id)
        {
            var leave = await _context.LeaveType.FirstOrDefaultAsync(n => n.Id == id);
            if (leave != null)
            {
                return leave;
            }
            else
            {
                throw new Exception("The leavetype doesn't exist");
            }
        }

        public async Task UpdateLeaveType(LeaveTypes leaveType)
        {
           
                _context.LeaveType.Update(leaveType);
                await _context.SaveChangesAsync();
        }
    }
}
