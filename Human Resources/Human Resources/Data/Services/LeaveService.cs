using Human_Resources.Data.ViewModels;
using Human_Resources.Models;
using Microsoft.EntityFrameworkCore;

namespace Human_Resources.Data.Services
{
    public class LeaveService : ILeaveService
    {
        private readonly AppDbContext _context;
        public LeaveService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddLeave(LeaveViewModel leave)
        {
            Leave persist = new Leave() 
            { 
                Id = leave.Id,
                LeaveType = leave.LeaveType,
                Remark = leave.Remark,
                EmployeeId = leave.EmployeeId,

            };
            await _context.Leaves.AddAsync(persist);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLeave(LeaveViewModel leave)
        {
            var toDelete = await _context.Leaves.FirstOrDefaultAsync(n => n.Id == leave.Id);
            if (toDelete != null)
            {
                _context.Leaves.Remove(toDelete);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("The asked Leave doesn't exist");
            }
        }

        public async Task<List<Leave>> GetAll()
        {
            var tabs = await _context.Leaves
                                     .Include(n=>n.Employee)
                                     .ToListAsync();
            return tabs;
        }

        public async Task<Leave> GetById(int id)
        {
            var leave = await _context.Leaves
                                       .Include(n => n.Employee)
                                       .FirstOrDefaultAsync(n => n.Id == id);
            if(leave != null)
            {
                return leave;
            }
            else
            {
                throw new Exception("The following Leave doesn't exist");
            }
        }

        public async Task<EmployeedropdownViewModel> GetEmployeedropdowns()
        {
            EmployeedropdownViewModel employeedropdownViewModel = new EmployeedropdownViewModel()
            {
                Employees = await _context.Employees.ToListAsync()
            };
            return employeedropdownViewModel;
        }

        public async Task UpdateLeave(LeaveViewModel leave)
        {
            var update = await _context.Leaves.FirstOrDefaultAsync(n => n.Id == leave.Id);
            if(update != null)
            {
                update.Id = leave.Id;
                update.Remark = leave.Remark;
                update.LeaveType = leave.LeaveType;
                update.EmployeeId = leave.EmployeeId;
                _context.Leaves.Update(update);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("This isn't a requested Leave");

            }
        }
        public async Task<Leave> SearchByEmployeeId(int id)
        {
            var leave = await _context.Leaves.FirstOrDefaultAsync(n => n.EmployeeId == id);
            if (leave != null)
            {
                return leave;
            }
            else
            {
                throw new Exception("The Leave doesn't exist");
            }
        }

    }
}
