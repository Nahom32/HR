using Human_Resources.Data.ViewModels;
using Human_Resources.Models;
using X.PagedList;

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
        public Task<Employee> GetEmployeeByEmail(string email);
        public Task<IPagedList<Employee>> getEmployees(int? page = 1);
        public Task<int> Count();
        public Task<List<Employee>> PaginatedEmployee(int val, int len);
        public Task<List<Employee>> SearchEmployees(DataTableRequest request);

    }
}
