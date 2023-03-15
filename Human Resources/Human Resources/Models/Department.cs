using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Number of Employees field is Required")]
        [Display(Name ="Number Of Employees")]
        public int NoOfEmployees { get; set; }
        [Required(ErrorMessage = "Department Name is Required")]
        [Display(Name = "Department Name")]
        public string DepartmentName { get; set; }
        //Relations
        //public List<Employee> Employees{ get; set; }
        public List<Branch> Branches { get; set; }
    }
}
