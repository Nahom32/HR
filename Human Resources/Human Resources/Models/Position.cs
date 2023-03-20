
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Models
{
    public class Position
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Position Name")]
        public string PositionName { get; set; }
        [Display(Name = "Position Salary")]
        public double PositionSalary { get; set; }

        //Relations
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

    }
}
