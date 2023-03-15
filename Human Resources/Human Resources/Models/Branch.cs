using System.ComponentModel.DataAnnotations;

namespace Human_Resources.Models
{
    public class Branch
    {
        [Key]
        public int Id { get; set; }
        public string BranchName { get; set; }
        List<Employee> Employees { get; set; }

    }
}
