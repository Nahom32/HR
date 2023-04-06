using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Models
{
    public class Reward
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The Reason for reward is required")]
        public string Reason { get; set; }
        [Required(ErrorMessage = "The amount paid for reward is required")]
        public double Amount { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        [Display(Name ="Employee Name")]
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

    }
}
