using Human_Resources.Data.ViewModels;
using Human_Resources.Models;

namespace Human_Resources.Data.Services
{
    public interface IRewardService
    {
        public Task<List<Reward>> GetAll();
        public Task<Reward> GetById(int id);
        public Task AddReward(RewardViewModel reward);
        public Task UpdateReward(RewardViewModel reward);
        public Task DeleteReward(RewardViewModel reward);
        public Task<EmployeedropdownViewModel> GetEmployeedropdowns();
        
    }
}
