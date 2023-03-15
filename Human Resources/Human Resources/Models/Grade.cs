using System.ComponentModel.DataAnnotations;

namespace Human_Resources.Models
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }
        public string GradeName { get; set; }

    }
}
