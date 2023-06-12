using Human_Resources.Models;

namespace Human_Resources.Data.Services
{
    public interface IHolidayService
    {
        public Task<List<Holiday>> GetAll();
        public Task<Holiday> GetById(int id);
        public Task AddHoliday(Holiday holiday);
        public Task UpdateHoliday(Holiday holiday);
        public Task DeleteHoliday(Holiday holiday);
    }
}
