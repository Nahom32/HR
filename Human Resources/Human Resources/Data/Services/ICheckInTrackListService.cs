using Human_Resources.Models;

namespace Human_Resources.Data.Services
{
    public interface ICheckInTrackListService
    {
        public Task<List<CheckInTrackList>> GetAll();
        public Task<CheckInTrackList> GetById(int id);
        public Task AddCheckInTrackList(CheckInTrackList checkInTrackList);
        public Task UpdateCheckInTrackList(CheckInTrackList checkInTrackList);
        public Task DeleteCheckInTrackList(CheckInTrackList checkInTrackList);
        public Task<List<CheckInTrackList>> GetByAttendance(int attendanceId);
    }
}
