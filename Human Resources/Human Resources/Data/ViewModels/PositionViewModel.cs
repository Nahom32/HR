using System.ComponentModel.DataAnnotations;

namespace Human_Resources.Data.ViewModels
{
    public class PositionViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The position name is required")]
        [Display(Name = "Position Name")]
        public string PositionName { get; set; }
        [Display(Name = "Position Salary")]
        public double PositionSalary { get; set; }
        [Display(Name = "Higher Position")]
        public int? PositionId { get; set; }
        [Display(Name = "Department")]
        public int? DepartmentId { get; set; }
    }
}
