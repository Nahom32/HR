using Human_Resources.Data.ViewModels;
using Human_Resources.Models;

namespace Human_Resources.Data.Services
{
    public interface IPromotionService
    {
        public Task<List<Promotion>> GetAll();
        public Task<Promotion> GetById(int id);
        public Task AddPromotion(PromotionViewModel promotion);
        public Task UpdatePromotion(PromotionViewModel promotion);
        public Task DeletePromotion(PromotionViewModel promotion);
        
        public Task<PositiondropdownViewModel> GetPositiondropdowns();
        public Task<EmployeedropdownViewModel> GetEmployeedropdowns();
        

    }
}
