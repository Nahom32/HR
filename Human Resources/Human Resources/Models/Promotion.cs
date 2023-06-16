using Human_Resources.Data.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Models
{
    public class Promotion
    {         
        public int Id { get; set; }
        [Required(ErrorMessage = "The reason for position change is required")]
        public string Reason { get; set; }
        [Required(ErrorMessage = "The type of Position change is Required")]
        public PositionChange PositionChange { get; set; }
        [Display(Name = "Previous Position")]
        [Required(ErrorMessage = "The previous position to be changed is Required")]
        public int fromPositionId { get; set; }
        [ForeignKey("fromPositionId")]
        public Position? PositionFrom { get; set; }
        [Display(Name = "New Position")]
        [Required(ErrorMessage = "The new position for promotion is Required ")]
        public int toPositionId { get; set;}
        [ForeignKey("toPositionId")]
        public Position? PositionTo { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }
        public DateTime PromotedAt { get; set; } = DateTime.Now;

    }
}
