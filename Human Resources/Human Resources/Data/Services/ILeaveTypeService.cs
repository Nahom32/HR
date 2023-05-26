using Human_Resources.Models;
namespace Human_Resources.Data.Services
{
    public interface ILeaveTypeService
    {
        public Task<List<LeaveTypes>> GetAll();
        public Task<LeaveTypes> GetById(int id);
        public Task AddLeaveType(LeaveTypes leaveType);
        public Task UpdateLeaveType(LeaveTypes leaveType);
        public Task DeleteLeaveType(int id);

    }
}
