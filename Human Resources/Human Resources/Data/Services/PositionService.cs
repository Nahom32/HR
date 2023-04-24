using Human_Resources.Models;
using Microsoft.EntityFrameworkCore;
using Human_Resources.Data.ViewModels;

namespace Human_Resources.Data.Services
{
    public class PositionService:IPositionService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PositionService> _logger;
        public PositionService(AppDbContext context, ILogger<PositionService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task AddPosition(PositionViewModel position)
        {
            Position pos = new Position()
            {
                Id = position.Id,
                DepartmentId = position.DepartmentId,
                PositionId = position.PositionId,
                PositionName = position.PositionName,
                PositionSalary =position.PositionSalary,

            };
            await _context.Positions.AddAsync(pos);
            await _context.SaveChangesAsync();
        }


        public async Task DeletePosition(PositionViewModel position)
        {

            var pos = await _context.Positions.FirstOrDefaultAsync(n=>n.Id==position.Id);

            if (pos != null)
            {
                _context.Positions.Remove(pos);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("The Value was not found ");
            }
        }

        public async Task<List<Position>> GetAll()
        {
            var retLis = await _context.Positions.Include(n => n.position).ToListAsync();
            return retLis;

        }

        public async Task<Position> GetById(int id)
        {
            var retval = await _context.Positions.Include(n => n.position).FirstOrDefaultAsync(n => n.Id == id);
            if (retval != null)
            {
                return retval;
            }
            else
            {
                throw new Exception($"The Value was not found with an id {id}");
            }
        }

        public async Task UpdatePosition(PositionViewModel position)
        {
            var pos = new Position()
            {
                Id = position.Id,
                PositionName = position.PositionName,
                PositionSalary = position.PositionSalary,
                PositionId = position.PositionId,
                DepartmentId = position.DepartmentId

            };
            _context.Positions.Update(pos);
            await _context.SaveChangesAsync();
            
        }
        public async Task<PositiondropdownViewModel> GetPositiondropdowns()
        {
            PositiondropdownViewModel positions = new PositiondropdownViewModel()
            {
                Positions = await _context.Positions.ToListAsync()
            };
            if(positions.Positions == null)
            {
                positions.Positions = new List<Position>();
            }
            return positions;
        }
        public async Task<DepartmentdropdownViewModel> GetDepartmentdropdowns()
        {
            DepartmentdropdownViewModel departments = new DepartmentdropdownViewModel()
            {
                Departments = await _context.Departments.ToListAsync()
            };
            if(departments.Departments == null)
            {
                departments.Departments = new List<Department>();
            }
            return departments;
        }
    }
}
