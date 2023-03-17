using Human_Resources.Models;

namespace Human_Resources.Data.Services
{
    public interface IEmployeeService
    {
        public Task<List<Employee>> GetAll();
        public Task<Employee> GetById(int id);
        public Task AddEmployee(Employee employee);
        public void UpdateDepartment(Employee employee);
        public void DeleteDepartment(Employee employee);
    }
}
