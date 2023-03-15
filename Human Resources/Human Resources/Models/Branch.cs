using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Models
{
    public class Branch
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Branch Name")]
        public string BranchName { get; set; }
        //Relations
        List<Employee> Employees { get; set; }
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

    }
}
