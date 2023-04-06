using System.ComponentModel.DataAnnotations;
using Human_Resources.Data.Enum;

namespace Human_Resources.Data.ViewModels
{
    public class AppraisalViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Punctuality is Required")]
        [Display(Name = "Punctuality")]
        public GradeValue Punctuality { get; set; }
        [Required(ErrorMessage = "The timeliness is Required ")]
        [Display(Name = "Timeliness")]
        public GradeValue Timeliness { get; set; }
        [Required(ErrorMessage = "The Group Work is Required ")]
        [Display(Name = "Group Work")]
        public GradeValue GroupWork { get; set; }
        [Required(ErrorMessage = "The technical skills field is required")]
        [Display(Name = "Technical Skills")]
        public GradeValue TechnicalSkills { get; set; }
        [Required(ErrorMessage = "The Collaborative Skills field is required")]
        [Display(Name = "Collaborative Skills")]
        public GradeValue CollaborativeSkills { get; set; }
        [Required(ErrorMessage = "The Employee is Required")]
        [Display(Name = "Employee Name")]

        public int EmployeeId { get; set; }
    }
}
