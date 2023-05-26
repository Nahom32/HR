using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Models
{
    public class Certificate
    {
        [Key]
        public string Name { get; set; }
        [Display(Name = "Start Date")]
        [Required(ErrorMessage ="The start date is required")]
        public DateOnly StartDate { get; set; }
        [Display(Name = "End Date")]
        [Required(ErrorMessage ="The End date is required")]
        public DateOnly EndDate { get; set; }
        [Required(ErrorMessage = "The Link is required")]
        public string Link;
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }
}
