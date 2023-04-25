using Human_Resources.Data.ViewModels;
using Human_Resources.Models;

namespace Human_Resources.Data.Services
{
    public interface ILeaveService
    {
        public Task<List<Leave>> GetAll();
        public Task<Leave> GetById(int id);
        public Task AddLeave(LeaveViewModel leave);
        public Task UpdateLeave(LeaveViewModel leave);
        public Task DeleteLeave(LeaveViewModel leave);
        public Task<EmployeedropdownViewModel> GetEmployeedropdowns();
        public Task<Leave> SearchByEmployeeId(int id);

    }
}
