using System.ComponentModel.DataAnnotations;

namespace Human_Resources.Models
{
    public class GradeCategory
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Category Name is Required")]
        [Display(Name ="Category Name")]
        public string CategoryName { get; set; }

    }
}
