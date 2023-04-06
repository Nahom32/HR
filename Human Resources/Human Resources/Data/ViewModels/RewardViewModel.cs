using System.ComponentModel.DataAnnotations;
using Human_Resources.Models;

namespace Human_Resources.Data.ViewModels
{
    public class RewardViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Reason for reward is required")]
        public string Reason { get; set; }
        [Required(ErrorMessage = "The amount paid for reward is required")]
        public double Amount { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;

        [Display(Name = "Employee Name")]
        public int EmployeeId { get; set; }
    }
}
