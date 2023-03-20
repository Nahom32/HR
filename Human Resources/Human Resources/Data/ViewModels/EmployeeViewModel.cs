using Human_Resources.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace Human_Resources.Data.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Marital Status is Required")]
        [Display(Name = "Marital Status")]
        public MaritalStatus MaritalStatus { get; set; }

        public string PhotoURL { get; set; }

        [Required(ErrorMessage = "Sex is Required")]
        public Sex Sex { get; set; }
        [Required(ErrorMessage = "Department is Required")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "Position is Required")]
        [Display(Name = "Department")]
        public int PositionId { get; set; }


    }

}
