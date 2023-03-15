using Human_Resources.Models;

namespace Human_Resources.Data.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly AppDbContext _context;
        public DepartmentService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddDepartment(Department department)
        {
             _context.Departments.Add(department);
        }

        public Task DeleteDepartment(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Department>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Department> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateDepartment(int id, Department department)
        {
            throw new NotImplementedException();
        }
    }
}
