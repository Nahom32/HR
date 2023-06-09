using System.ComponentModel.DataAnnotations;

namespace Human_Resources.Models
{
    public class Holiday
    {
        [Key]
        public int Id { get; set; }
        public string HolidayName { get; set; }
        public int Month { get; set; }
        public int Date { get; set; }


    }
}
