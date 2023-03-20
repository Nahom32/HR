using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Models
{
    public class Employee_Position
    {
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }
        public int PositionId { get; set; }
        [ForeignKey("PositionId")]
        public Position Position { get; set; }
    }
}
