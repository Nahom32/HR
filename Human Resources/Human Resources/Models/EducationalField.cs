using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Models
{
    public class EducationalField
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }
        public int PositionId { get; set; }
        [ForeignKey("PositionId")]
        public Position Position { get; set; }
    }
}
