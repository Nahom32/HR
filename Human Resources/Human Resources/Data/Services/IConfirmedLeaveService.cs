using Human_Resources.Data.ViewModels;
using Human_Resources.Models;

namespace Human_Resources.Data.Services
{
    public interface IConfirmedLeaveService
    {
        public Task<List<ConfirmedLeave>> GetAll();
        public Task<ConfirmedLeave> GetById(int id);
        public Task AddConfirmedLeave(ConfirmedLeave confirmedLeave);
        public Task UpdateConfirmedLeave(ConfirmedLeave confirmedLeave);
        public Task DeleteLeave(ConfirmedLeave confirmedLeave);
    }
}
