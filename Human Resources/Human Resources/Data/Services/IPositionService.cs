using Human_Resources.Models;

namespace Human_Resources.Data.Services
{
    public interface IPositionService
    {
        public Task<List<Position>> GetAll();
        public Task<Position> GetById(int id);
        public Task AddPosition(Position position);
        public void UpdatePosition(Position position);
        public void DeletePosition(Position position);
    }
}
