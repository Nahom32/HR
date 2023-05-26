using Human_Resources.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Human_Resources.Data.ViewModels
{
    public class ConfirmedLeaveModel
    {
        [Key]
        public int Id { get; set; }
        public string Remark { get; set; }
        public int LeaveTypesId { get; set; }
        public int EmployeeId { get; set; }
        
    }
}
