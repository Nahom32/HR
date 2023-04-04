using Human_Resources.Data.Enum;
using Human_Resources.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Data.ViewModels
{
    public class PromotionViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Reason is Required")]
        public string Reason { get; set; }
        [Display(Name = "Type of Change")]
        [Required(ErrorMessage = "The type of Position change is Required")]
        public PositionChange PositionChange { get; set; }
        [Display(Name = "Previous Position")]
        [Required(ErrorMessage = "The previous position to be changed is Required")]
        public int fromPositionId { get; set; }
       
        public Position? PositionFrom { get; set; }
        [Display(Name = "New Position")]
        [Required(ErrorMessage ="The new position for promotion is Required ")]
        public int toPositionId { get; set; }
        
        public Position? PositionTo { get; set; }
        [Display(Name = "Employee Name")]
        [Required(ErrorMessage = "The Employee must be chosen")]
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
}
