using Human_Resources.Models;
using Human_Resources.Data.ViewModels;

namespace Human_Resources.Data.Services
{
    public interface IPositionService
    {
        public Task<List<Position>> GetAll();
        public Task<Position> GetById(int id);
        public Task AddPosition(PositionViewModel position);
        public Task UpdatePosition(PositionViewModel position);
        public Task DeletePosition(PositionViewModel position);
        public Task<PositiondropdownViewModel> GetPositiondropdowns();
        public Task<DepartmentdropdownViewModel> GetDepartmentdropdowns();
    }
}
