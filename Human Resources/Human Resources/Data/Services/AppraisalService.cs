using Human_Resources.Data.ViewModels;
using Human_Resources.Models;
using Microsoft.EntityFrameworkCore;

namespace Human_Resources.Data.Services
{
    public class AppraisalService : IAppraisalService
    {
        private readonly AppDbContext _context;
        public AppraisalService(AppDbContext context)
        {

            _context = context;

        }
        public async Task AddAppraisal(AppraisalViewModel appraisal)
        {
            Appraisal grade = new Appraisal()
            {
                Id = appraisal.Id,
                Punctuality = appraisal.Punctuality,
                Timeliness = appraisal.Timeliness,
                TechnicalSkills = appraisal.TechnicalSkills,
                CollaborativeSkills = appraisal.CollaborativeSkills,
                GroupWork = appraisal.GroupWork,
                EmployeeId = appraisal.EmployeeId,
            };
            await _context.Appraisals.AddAsync(grade);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAppraisal(AppraisalViewModel appraisal)
        {
            var toDelete = await _context.Appraisals.FirstOrDefaultAsync(n=>n.Id==appraisal.Id);
            if(toDelete != null)
            {
                _context.Appraisals.Remove(toDelete);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("the value doesn't exist");
            }
        }

        public async Task<List<Appraisal>> GetAll()
        {
            var retLis = await _context.Appraisals
                                       .Include(n => n.Employee)
                                        .ToListAsync();
            return retLis;
        }

        public async Task<Appraisal> GetById(int id)
        {
            var appraisal = await _context.Appraisals
                                        .Include(n => n.Employee)
                                        .FirstOrDefaultAsync(n => n.Id == id);
            if (appraisal != null)
            {
                return appraisal;
            }
            else
            {
                throw new Exception("appraisal doesn't exist");
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

        public async Task UpdateAppraisal(AppraisalViewModel appraisal)
        {
            Appraisal grade = new Appraisal()
            {
                Id = appraisal.Id,
                Punctuality = appraisal.Punctuality,
                Timeliness = appraisal.Timeliness,
                TechnicalSkills = appraisal.TechnicalSkills,
                CollaborativeSkills = appraisal.CollaborativeSkills,
                GroupWork = appraisal.GroupWork,
                EmployeeId = appraisal.EmployeeId,
            };
            _context.Appraisals.Update(grade);
            await _context.SaveChangesAsync(); 
        }
    }
}
