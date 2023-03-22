using Human_Resources.Data.ViewModels;
using Human_Resources.Models;

namespace Human_Resources.Data.Services
{
    public interface IEmployeeService
    {
        public Task<List<Employee>> GetAll();
        public Task<Employee> GetById(int id);
        public Task AddEmployee(EmployeeViewModel employee);
        public Task UpdateEmployee(EmployeeViewModel employee);
        public Task DeleteEmployee(EmployeeViewModel employee);
        public Task<DepartmentdropdownViewModel> GetDepartmentdropdowns();
        public Task<PositiondropdownViewModel> GetPositiondropdowns();
        public  Task<EducationalFielddropdownViewModel> GetEducationalFielddropdowns();
    }
}
