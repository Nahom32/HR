using Human_Resources.Data.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        [Display(Name = "Photo")]
        public string  PhotoURL { get; set; }
        [Required(ErrorMessage ="Sex is Required")]
        public Sex Sex { get; set; }
        public EducationalLevel EducationalLevel { get; set; }
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]

        public Department Department { get; set; }
        public int PositionId { get; set; }
        [ForeignKey("PositionId")]
        public Position Position { get; set; }

        public int EducationalFieldId { get; set; }
        [ForeignKey("EducationalFieldId")]
        public EducationalField EducationalField{ get; set; }
        [Display(Name = "Role")]
        public Roles Roles { get; set; }

        

    }
}
