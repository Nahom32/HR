using System.ComponentModel.DataAnnotations;

namespace Human_Resources.Data.Helpers
{
    public class Configuration
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Hours of Work Required")]
        public int HoursOfWork { get; set; }
        [Display(Name = "Percent Decrease for Absent Employees")]
        public double percentDecreaseAbsent { get; set; }
        [Display(Name = "Percent Decrease for Late Employees")]
        public double percentDecreaseLate { get; set;}
        [Display(Name = "Attendance Sync Time")]
        public DateTime AttendanceSyncTime { get; set; }
        [Display(Name = "Encashment Sync Date")]
        public DateTime LeaveEncashmentSyncDate { get; set; }




    }
}
