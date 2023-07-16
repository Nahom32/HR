using Human_Resources.Models;

namespace Human_Resources.Data.Services
{
    public interface ICheckOutTrackListService
    {
        public Task<List<CheckOutTrackList>> GetAll();
        public Task<CheckOutTrackList> GetById(int id);
        public Task AddCheckOutTrackList(CheckOutTrackList checkInTrackList);
        public Task UpdateCheckOutTrackList(CheckOutTrackList checkInTrackList);
        public Task DeleteCheckOutTrackList(CheckOutTrackList checkInTrackList);
        public Task<List<CheckOutTrackList>> GetByCheckInTrackList(int checkInId);
    }
}
