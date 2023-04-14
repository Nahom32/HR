
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Models
{
    public class Position
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="The position name is required")]
        [Display(Name ="Position Name")]
        public string PositionName { get; set; }
        [Display(Name = "Position Salary")]
        public double PositionSalary { get; set; }
        public int? PositionId { get; set; }
        [ForeignKey("PositionId")]
        public Position? position { get; set; }
        public int? DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        //Relations

    }
}
