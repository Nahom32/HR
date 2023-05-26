using Human_Resources.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Human_Resources.Data.ViewModels
{
    public class RejectedLeaveModel
    {
       
        public string Remark { get; set; }
        public string RefusalReason { get; set; }
        public int LeaveTypesId { get; set; }

        public int EmployeeId { get; set; }
        
    }
}
