using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Models
{
    public class ConfirmedLeave
    {
        [Key]
        public int Id { get; set; }
        public string Remark { get; set; }
        public int LeaveTypesId { get; set; }
        [ForeignKey("LeaveTypesId")]
        public LeaveTypes? LeaveTypes { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }
        public DateTime ConfirmedAt { get; set; } = DateTime.Now;
        public int NoOfDays { get; set; }
        public int LeaveId { get; set; }

    }
}
