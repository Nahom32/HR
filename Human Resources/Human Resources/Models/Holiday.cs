using System.ComponentModel.DataAnnotations;

namespace Human_Resources.Models
{
    public class Holiday
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string HolidayName { get; set; }
        [Required]
        [Range(1,12)]
        public int Month { get; set; }
        [Required]
        [Range(1,31)]
        public int Date { get; set; }


    }
}
