using Human_Resources.Models;
using Microsoft.EntityFrameworkCore;

namespace Human_Resources.Data.Services
{
    public class RejectedLeaveService : IRejectedLeaveService
    {
        private readonly AppDbContext _context;
        public RejectedLeaveService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddRejectedLeave(RejectedLeave confirmedLeave)
        {
            await _context.RejectedLeaves.AddAsync(confirmedLeave);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRejectedLeave(RejectedLeave confirmedLeave)
        {
            _context.RejectedLeaves.Remove(confirmedLeave);
            await _context.SaveChangesAsync();
        }

        public async Task<List<RejectedLeave>> GetAll()
        {
            var results = await _context.RejectedLeaves.Include(n => n.Employee).ToListAsync();
            return results;
        }

        public async Task<RejectedLeave> GetById(int id)
        {
            var obj = await _context.RejectedLeaves
                                    .Include(n => n.Employee)
                                    .FirstOrDefaultAsync(n => n.Id == id);
            if (obj != null)
            {
                return obj;
            }
            else
            {
                throw new Exception("There is no leave confirmed to this day");
            }
        }

        public async Task UpdateRejectedLeave(RejectedLeave confirmedLeave)
        {
            _context.RejectedLeaves.Update(confirmedLeave);
            await _context.SaveChangesAsync();
        }
    }
}

