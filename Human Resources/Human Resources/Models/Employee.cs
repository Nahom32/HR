using Human_Resources.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Marital Status is Required")]
        [Display(Name = "Marital Status")]
        public MaritalStatus MaritalStatus { get; set; }
        public string  PhotoURL { get; set; }
        [Required(ErrorMessage ="Sex is Required")]
        public Sex Sex { get; set; }
        public int PositionId { get; set; }
        [ForeignKey("PositionId")]
        [Required(ErrorMessage = "Position is Required")]
        public Position Position { get; set; }
        //public int DepartmentId { get; set; }
        //[ForeignKey("DepartmentId")]
        //[Required(ErrorMessage = "Department is Required")]
        //public Department Department { get; set; }
        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }

    }
}
