using Human_Resources.Data.Enum;
using System.ComponentModel.DataAnnotations;
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
        public Employee? Employee { get; set; }
        [Display(Name ="Number of Days")]
        [Required(ErrorMessage ="The number of days is Required")]
        public DateTime AskedAt { get; set; } = DateTime.Now;


    }
}
