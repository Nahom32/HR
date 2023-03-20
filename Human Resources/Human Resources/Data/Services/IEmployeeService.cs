using Human_Resources.Data.ViewModels;
using Human_Resources.Models;

namespace Human_Resources.Data.Services
{
    public interface IEmployeeService
    {
        public Task<List<Employee>> GetAll();
        public Task<Employee> GetById(int id);
        public Task AddEmployee(EmployeeViewModel employee);
        public void UpdateEmployee(EmployeeViewModel employee);
        public void DeleteEmployee(EmployeeViewModel employee);
        public Task<DepartmentdropdownViewModel> GetDepartmentdropdowns();

    }
}
