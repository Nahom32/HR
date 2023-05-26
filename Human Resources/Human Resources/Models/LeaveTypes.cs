using System.ComponentModel.DataAnnotations;

namespace Human_Resources.Models
{
    public class LeaveTypes
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="The Leave field is required")]
        [Display(Name = "Leave Name")]
        public string LeaveName { get; set; }
        [Required(ErrorMessage = "The days field is required")]
        [Display(Name = "Number of Days")]
        public int Days { get; set; }
    }
}
