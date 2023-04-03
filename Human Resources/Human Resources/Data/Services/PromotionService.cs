using Human_Resources.Data.ViewModels;
using Human_Resources.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Human_Resources.Data.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly AppDbContext _context;
        public PromotionService(AppDbContext context)
        {
            _context = context;
            
        }
        public async Task AddPromotion(PromotionViewModel promotion)
        {
            Promotion promo = new Promotion()
            {
                Id = promotion.Id,
                Reason = promotion.Reason,
                fromPositionId = promotion.fromPositionId,
                toPositionId = promotion.toPositionId,
                EmployeeId = promotion.EmployeeId,
                PositionChange = promotion.PositionChange,
            };
            await _context.Promotions.AddAsync(promo);
            await _context.SaveChangesAsync();    
        }

        public async Task DeletePromotion(PromotionViewModel promotion)
        {
            var toDelete = await _context.Promotions.FirstOrDefaultAsync(n => n.Id == promotion.Id);
            if (toDelete != null)
            {
                _context.Promotions.Remove(toDelete);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("The described Promotion doesn't exist");
            }
        }

        public async Task<List<Promotion>> GetAll()
        {
            var retLis = await _context.Promotions
                .Include(n => n.Employee)
                .Include(n => n.PositionFrom)
                .Include(n => n.PositionTo)
                .ToListAsync();
            return retLis;
        }

        public async Task<Promotion> GetById(int id)
        {
            var showPromotion = await _context.Promotions
                                        .Include(n => n.Employee)
                                        .Include(n => n.PositionFrom)
                                        .Include(n => n.PositionTo)
                                        .FirstOrDefaultAsync(n => n.Id == id);
            if (showPromotion != null)
            {
                return showPromotion;
            }
            else
            {
                throw new Exception("Such a value doesn't exist");
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

        public async Task<PositiondropdownViewModel> GetPositiondropdowns()
        {
            var response = new PositiondropdownViewModel()
            {
                Positions = await _context.Positions.ToListAsync()
            };
            return response;
        }

        public async Task UpdatePromotion(PromotionViewModel promotion)
        {
            Promotion updatePromotion = new Promotion()
            {
                Id = promotion.Id,
                Reason = promotion.Reason,
                fromPositionId = promotion.fromPositionId,
                toPositionId = promotion.toPositionId,
                EmployeeId = promotion.EmployeeId,
                PositionChange = promotion.PositionChange,

            };
            _context.Promotions.Update(updatePromotion);
            await _context.SaveChangesAsync();
        }
    }
}
