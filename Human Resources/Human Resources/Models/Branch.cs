using System.ComponentModel.DataAnnotations;

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

    }
}
