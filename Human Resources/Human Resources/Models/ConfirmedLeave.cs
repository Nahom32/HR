using Human_Resources.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Models
{
    public class ConfirmedLeave
    {
        public int Id { get; set; }
        public string Remark { get; set; }
        public LeaveType LeaveType { get; set; }
        public int LeaveId { get; set; }
        [ForeignKey("LeaveId")]
        public Leave Leave { get; set; }
        public DateTime ConfirmedAt { get; set; } = DateTime.Now;

    }
}
