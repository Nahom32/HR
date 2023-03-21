using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Models
{
    public class Employee_EducationalField
    {
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public int EducationalFieldId { get; set; }
        [ForeignKey("EducationalFieldId")]
        public EducationalField EducationalField { get; set; }

    }
}
