using Human_Resources.Models;

namespace Human_Resources.Data.ViewModels
{
    public class LeaveTypedropdownViewModel
    {
        public LeaveTypedropdownViewModel()
        {
            LeaveTypes = new List<LeaveTypes>();
        }
        public List<LeaveTypes> LeaveTypes { get; set; }
    }
}
