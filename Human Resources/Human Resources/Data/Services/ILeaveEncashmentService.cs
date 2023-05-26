using Human_Resources.Models;

namespace Human_Resources.Data.Services
{
    public interface ILeaveEncashmentService
    {
        public Task<List<LeaveEncashment>> GetAll();
        public Task<LeaveEncashment> GetById(int id);
        public Task AddLeaveEncashment(LeaveEncashment leaveEncashment);
        public Task UpdateLeaveEncashment(LeaveEncashment leaveEncashment);
        public Task DeleteLeaveEncashment(int id);
        public Task<LeaveEncashment> GetByEmployeeId(int employeeId);
    }
}
