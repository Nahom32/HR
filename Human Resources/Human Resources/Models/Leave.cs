
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Human_Resources.Data.Enum;

namespace Human_Resources.Models
{
    public class Leave
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
        [Display(Name ="Number of Days")]
        [Required(ErrorMessage ="The number of days is Required")]
        public DateTime AskedAt { get; set; } = DateTime.Now;
        public LeaveStatus LeaveStatus { get; set; } = LeaveStatus.Pending;


    }
}
