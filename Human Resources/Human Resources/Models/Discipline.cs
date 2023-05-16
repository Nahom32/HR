using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Human_Resources.Data.Enum;

namespace Human_Resources.Models
{
    public class Discipline
    {
        [Key]
        public int Id { get; set; }
        public DisciplineType DiscplineType { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

    }
}
