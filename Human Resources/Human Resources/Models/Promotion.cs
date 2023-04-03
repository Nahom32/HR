using Human_Resources.Data.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Models
{
    public class Promotion
    {         
        public int Id { get; set; }
        public string Reason { get; set; }
        public PositionChange PositionChange { get; set; }
        public int fromPositionId { get; set; }
        [ForeignKey("fromPositionId")]
        public Position PositionFrom { get; set; }
        public int toPositionId { get; set;}
        [ForeignKey("toPositionId")]
        public Position PositionTo { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

    }
}
