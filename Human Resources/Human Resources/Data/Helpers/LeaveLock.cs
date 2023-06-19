using System.ComponentModel.DataAnnotations;

namespace Human_Resources.Data.Helpers
{
    public class LeaveLock
    {

        [Key]
        public int Id { get; set; }
        public DateTime lockTime { get; set; } = DateTime.Now;
    }
}
