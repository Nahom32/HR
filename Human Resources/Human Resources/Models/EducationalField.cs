using System.ComponentModel.DataAnnotations;

namespace Human_Resources.Models
{
    public class EducationalField
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }

    }
}
