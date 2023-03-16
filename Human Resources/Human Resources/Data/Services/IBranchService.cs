using Human_Resources.Models;

namespace Human_Resources.Data.Services
{
    public interface IBranchService
    {
        public Task<Branch> GetByIdAsync(int id);
        public Task<List<Branch>> GetAllAsync();
        public Task DeleteAsync(int id);
        public Task<Branch> UpdateAsync(int id, Branch branch);


    }
}
