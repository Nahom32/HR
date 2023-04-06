using Human_Resources.Data.ViewModels;
using Human_Resources.Models;

namespace Human_Resources.Data.Services
{
    public interface IAppraisalService
    {
        public Task<List<Appraisal>> GetAll();
        public Task<Appraisal> GetById(int id);
        public Task AddAppraisal(AppraisalViewModel appraisal);
        public Task UpdateAppraisal(AppraisalViewModel appraisal);
        public Task DeleteAppraisal(AppraisalViewModel appraisal);
        public Task<EmployeedropdownViewModel> GetEmployeedropdowns();
    }
}
