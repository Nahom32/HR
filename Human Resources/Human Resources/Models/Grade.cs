using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Models
{
    public class Grade
    {
        [Key]
        public int Id { get; set; }
        public string GradeName { get; set; }
        public int GradeCategoryId { get; set; }
        [ForeignKey("GradeCategoryId")]
        public GradeCategory GradeCategory { get; set; }


    }
}
