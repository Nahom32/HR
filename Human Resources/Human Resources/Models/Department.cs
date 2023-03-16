using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The description of the department is required")]
        [Display(Name ="Department Description")]
        public string DepartmentDescription { get; set; }
        [Required(ErrorMessage = "Department Name is Required")]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
        public List<Branch> Branches { get; set; }
    }
}
