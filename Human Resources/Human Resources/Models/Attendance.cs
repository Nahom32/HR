using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Models
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public int NoOnTimeCheck { get; set; } = 0;
        public int NoOfLateCheck { get; set; } = 0;
        public int NoOfAbsentCheck { get; set; } = 0;



    }
}
