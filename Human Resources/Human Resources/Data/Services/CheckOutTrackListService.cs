using Human_Resources.Models;
using Microsoft.EntityFrameworkCore;

namespace Human_Resources.Data.Services
{
    public class CheckOutTrackListService:ICheckOutTrackListService
    {
        private readonly AppDbContext _context;
        public CheckOutTrackListService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddCheckOutTrackList(CheckOutTrackList checkOutTrackList)
        {
            _context.CheckOutTrackLists.Add(checkOutTrackList);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCheckOutTrackList(CheckOutTrackList checkOutTrackList)
        {
            _context.CheckOutTrackLists.Remove(checkOutTrackList);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CheckOutTrackList>> GetAll()
        {
            var all = await _context.CheckOutTrackLists.Include(n=>n.CheckInTrackList).ToListAsync();
            return all;
        }

        public async Task<CheckOutTrackList> GetById(int id)
        {
            var checkIn = await _context.CheckOutTrackLists.FirstOrDefaultAsync(n => n.Id == id);
            if (checkIn != null)
            {
                return checkIn;
            }
            else
            {
                throw new Exception("Error not found");
            }
        }

        public async Task UpdateCheckOutTrackList(CheckOutTrackList checkOutTrackList)
        {
            _context.CheckOutTrackLists.Update(checkOutTrackList);
            await _context.SaveChangesAsync();
        }
        //public async Task<List<CheckInTrackList>> GetByCheckInTrackList(int checkInId)
        //{
        //    var total = await _context.CheckOutTrackLists.Where(n=>n.Id == checkInId).ToListAsync();
        //    return total;
        //}

    }
}
