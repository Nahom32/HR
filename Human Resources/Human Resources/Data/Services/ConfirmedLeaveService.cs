using Human_Resources.Models;
using Microsoft.EntityFrameworkCore;

namespace Human_Resources.Data.Services
{
    public class ConfirmedLeaveService : IConfirmedLeaveService
    {
        private readonly AppDbContext _context;
        public ConfirmedLeaveService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddConfirmedLeave(ConfirmedLeave confirmedLeave)
        {
            await _context.ConfirmedLeaves.AddAsync(confirmedLeave);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLeave(ConfirmedLeave confirmedLeave)
        {
            _context.ConfirmedLeaves.Remove(confirmedLeave);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ConfirmedLeave>> GetAll()
        {
            var results = await _context.ConfirmedLeaves.Include(n => n.Employee).ToListAsync();
            return results;
        }

        public async Task<ConfirmedLeave> GetById(int id)
        {
            var obj = await _context.ConfirmedLeaves
                                    .Include(n=>n.Employee)
                                    .FirstOrDefaultAsync(n => n.Id == id);
            if(obj != null)
            {
                return obj;
            }
            else
            {
                throw new Exception("There is no leave confirmed to this day");
            }
        }

        public async Task UpdateConfirmedLeave(ConfirmedLeave confirmedLeave)
        {
            _context.ConfirmedLeaves.Update(confirmedLeave);
            await _context.SaveChangesAsync();
        }
    }
}
