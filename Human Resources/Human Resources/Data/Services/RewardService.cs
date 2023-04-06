using Human_Resources.Data.ViewModels;
using Human_Resources.Models;
using Microsoft.EntityFrameworkCore;

namespace Human_Resources.Data.Services
{
    public class RewardService:IRewardService
    {
        private readonly AppDbContext _context;
        public RewardService(AppDbContext context)
        {

            _context = context;

        }

        public async Task AddReward(RewardViewModel reward)
        {
            Reward persist = new Reward()
            {
                Id = reward.Id,
                Reason= reward.Reason,
                Amount = reward.Amount,
                DateTime = reward.DateTime,
                EmployeeId = reward.EmployeeId,
            };
            await _context.Rewards.AddAsync(persist);
            await _context.SaveChangesAsync();
           
        }

        public async Task DeleteReward(RewardViewModel reward)
        {
            var persist =await _context.Rewards.FirstOrDefaultAsync(n => n.Id == reward.Id);
            if( persist != null)
            {
                _context.Rewards.Remove(persist);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("The following Reward doesn't exist");
            }
            
        }

        public async Task<List<Reward>> GetAll()
        {
            var rewards = await _context.Rewards
                                        .Include(n=>n.Employee)
                                        .ToListAsync();
            return rewards;
        }

        public async Task<Reward> GetById(int id)
        {
            var value = await _context.Rewards
                                      .Include(n=>n.Employee)
                                      .FirstOrDefaultAsync(n => n.Id == id);
            if(value != null)
            {
                return value;
            }
            else
            {
                throw new Exception("The Reward doesn't exist");
            }
            
        }

        public async Task<EmployeedropdownViewModel> GetEmployeedropdowns()
        {
            var response = new EmployeedropdownViewModel()
            {
                Employees = await _context.Employees.ToListAsync()
            };
            return response;
        }

        public async Task UpdateReward(RewardViewModel reward)
        {
            Reward update = new Reward()
            {
                Id = reward.Id,
                EmployeeId = reward.EmployeeId,
                Reason = reward.Reason,
                Amount = reward.Amount,
                DateTime = reward.DateTime,


            };
            _context.Rewards.Update(update);
            await _context.SaveChangesAsync();

        }
    }
}
