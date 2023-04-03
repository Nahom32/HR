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
        public int Amount { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        [ForeignKey("EmployeeId")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }


    }
}
