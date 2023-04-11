using Human_Resources.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Models
{
    public class Leave
    {
        public int Id { get; set; }
        public string Remark { get; set; }
        public LeaveType LeaveType { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public DateTime AskedAt { get; set; } = DateTime.Now;


    }
}
