using Human_Resources.Models;
using Microsoft.EntityFrameworkCore;

namespace Human_Resources.Data.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly AppDbContext _context;
        public HolidayService(AppDbContext context)
        {

            _context = context;

        }
        public async Task AddHoliday(Holiday holiday)
        {
            var holidayFind = await _context.Holidays.FirstOrDefaultAsync(n => n.HolidayName == holiday.HolidayName);
            if(holidayFind == null )
            {
                await _context.Holidays.AddAsync(holiday);
                await _context.SaveChangesAsync();

            }

        }

        public async Task DeleteHoliday(Holiday holiday)
        {
            var holidayFind = await _context.Holidays.FirstOrDefaultAsync(n => n.HolidayName == holiday.HolidayName);
            if (holidayFind != null)
            {
                _context.Holidays.Remove(holiday);
                await _context.SaveChangesAsync();

            }
        }

        public async Task<List<Holiday>> GetAll()
        {
            return await _context.Holidays.ToListAsync();
        }

        public async Task<Holiday> GetById(int id)
        {
            var value = await _context.Holidays.FirstOrDefaultAsync(n => n.Id == id);
            if(value != null)
            {
                return value;
            }
            else
            {
                throw new Exception($"the Holiday with the id: {id} doesn't exist");
            }
        }

        public async Task UpdateHoliday(Holiday holiday)
        {
            _context.Holidays.Update(holiday);
            await _context.SaveChangesAsync();
        }
    }

}
