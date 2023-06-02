using Human_Resources.Models;

namespace Human_Resources.Data.ViewModels
{
    public class AttendanceViewModel
    {
        public AttendanceViewModel()
        {
            CheckInTracks = new List<CheckInTrackList>();  
        }
        public Attendance Attendance { get; set; }
        public List<CheckInTrackList> CheckInTracks { get; set; }
        public DateTime LastTime { get; set; }
    }
}
