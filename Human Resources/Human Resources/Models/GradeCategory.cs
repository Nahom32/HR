using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Models
{
    public class GradeCategory
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Category Name is Required")]
        [Display(Name ="Category Name")]
        public string CategoryName { get; set; }
        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }

    }
}
