using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Models
{
    public class EducationalField
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
    }
}
