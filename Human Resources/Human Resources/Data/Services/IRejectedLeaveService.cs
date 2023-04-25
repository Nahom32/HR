using Human_Resources.Models;

namespace Human_Resources.Data.Services
{
    public interface IRejectedLeaveService
    {
        public Task<List<RejectedLeave>> GetAll();
        public Task<RejectedLeave> GetById(int id);
        public Task AddRejectedLeave(RejectedLeave rejectedLeave);
        public Task UpdateRejectedLeave(RejectedLeave rejectedLeave);
        public Task DeleteRejectedLeave(RejectedLeave rejectedLeave);
    }
}
