using Human_Resources.Models;
using Microsoft.EntityFrameworkCore;

namespace Human_Resources.Data.Services
{
    public class CheckInTrackListService : ICheckInTrackListService
    {
        private readonly AppDbContext _context;
        public CheckInTrackListService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddCheckInTrackList(CheckInTrackList checkInTrackList)
        {
            _context.CheckInTrackLists.Add(checkInTrackList);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCheckInTrackList(CheckInTrackList checkInTrackList)
        {
            _context.CheckInTrackLists.Remove(checkInTrackList);
            await _context.SaveChangesAsync();
        }

        public async Task<List<CheckInTrackList>> GetAll()
        {
            var all = await _context.CheckInTrackLists.ToListAsync();
            return all;
        }

        public async Task<CheckInTrackList> GetById(int id)
        {
            var checkIn = await _context.CheckInTrackLists.FirstOrDefaultAsync(n => n.Id == id);
            if(checkIn != null)
            {
                return checkIn;
            }
            else
            {
                throw new Exception("Error not found");
            }
        }

        public async Task UpdateCheckInTrackList(CheckInTrackList checkInTrackList)
        {
            _context.CheckInTrackLists.Update(checkInTrackList);
            await _context.SaveChangesAsync();
        }
        public async Task<List<CheckInTrackList>> GetByAttendance(int attendanceId)
        {
            var attendances = new List<CheckInTrackList>();
            var total = await _context.CheckInTrackLists.ToListAsync();
            foreach(var checkIn in total)
            {
                if(checkIn.AttendanceId == attendanceId)
                {
                    attendances.Add(checkIn);
                }
            }
            return attendances;
        }
    }
}
