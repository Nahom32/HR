using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Models
{
    public class Allowance
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The Name is Requried")]
        public string Name { get; set; }
        [Required(ErrorMessage ="The Amount is Required")]
        public double Amount { get; set; }
        private int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

    }
}
