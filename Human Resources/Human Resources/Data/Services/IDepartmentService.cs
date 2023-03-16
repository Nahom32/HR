using Human_Resources.Models;

namespace Human_Resources.Data.Services
{
    public interface IDepartmentService
    {
        public Task<List<Department>> GetAll();
        public Task<Department> GetById(int id);
        public Task AddDepartment(Department department);
        public void UpdateDepartment(Department department);
        public void DeleteDepartment(Department department);


    }
}
