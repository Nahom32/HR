using Human_Resources.Models;
using Microsoft.EntityFrameworkCore;

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
        public async Task AddPosition(Position position)
        {
            await _context.Positions.AddAsync(position);
            await _context.SaveChangesAsync();
        }


        public void DeletePosition(Position position)
        {

            if (position != null)
            {
                _context.Positions.Remove(position);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("The Value was not found ");
            }
        }

        public async Task<List<Position>> GetAll()
        {
            var retLis = await _context.Positions.ToListAsync();
            return retLis;

        }

        public async Task<Position> GetById(int id)
        {
            var retval = await _context.Positions.FindAsync(id);
            if (retval != null)
            {
                return retval;
            }
            else
            {
                throw new Exception($"The Value was not found with an id {id}");
            }
        }

        public void UpdatePosition(Position position)
        {
            _context.Positions.Update(position);
            _context.SaveChanges();

        }
    }
}
