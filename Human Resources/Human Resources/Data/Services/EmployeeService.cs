using Human_Resources.Models;

namespace Human_Resources.Data.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;  
        }
        public Task AddEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void DeleteDepartment(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task<List<Employee>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateDepartment(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
