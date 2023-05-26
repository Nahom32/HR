using Human_Resources.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Models
{
    public class CheckInTrackList
    {
        [Key]
        public int Id { get; set; }
        public int AttendanceId { get; set; }
        [ForeignKey("AttendanceId")]
        public Attendance Attendance { get; set; }
        public DateTime CheckInTime { get; set; }
        public CheckInStatus checkInStatus { get; set; }

        
    }
}
