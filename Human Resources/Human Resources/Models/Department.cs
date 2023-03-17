using Human_Resources.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Department Name")]
        
        [Required(ErrorMessage = "Department Name is Required")]
        public string DepartmentName { get; set; }
        [Display(Name = "Department Description")]
        [Required(ErrorMessage = "The description of the department is required")]
        public string DepartmentDescription { get; set; }

    }
}
