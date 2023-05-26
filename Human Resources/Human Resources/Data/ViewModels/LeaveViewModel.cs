using Human_Resources.Data.Enum;

namespace Human_Resources.Data.ViewModels
{
    public class LeaveViewModel
    {
        public int Id { get; set; }
        public string Remark { get; set; }
        public int LeaveTypesId { get; set; }
        public int EmployeeId { get; set; }
        public LeaveStatus LeaveStatus { get; set; } = LeaveStatus.Pending;
    }
}
