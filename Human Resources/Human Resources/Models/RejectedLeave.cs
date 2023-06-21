using Human_Resources.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Models
{
    public class RejectedLeave
    {
        [Key]
        public int Id { get; set; }
        public string Remark { get; set; }
        public string RefusalReason { get; set; }
        public int LeaveTypesId { get; set; }
        [ForeignKey("LeaveTypesId")]
        public LeaveTypes? LeaveTypes { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }
        public DateTime RejectedAt { get; set; } = DateTime.Now;
        public int NoOfDays { get; set; }
        public int LeaveId { get; set; }

    }
}
