using Human_Resources.Data.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public Sex Sex { get; set; }
        public int PositionId { get; set; }
        [ForeignKey("PositionId")]
        public Position Position { get; set; }

    }
}
