using Human_Resources.Data.ViewModels;
using Human_Resources.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Human_Resources.Data.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _context;

        public EmployeeService(AppDbContext context)
        {
            _context = context;  
        }
        public async Task AddEmployee(EmployeeViewModel employeeViewModel)
        {
            var employee = new Employee()
            {
                Id = employeeViewModel.Id,
                Email = employeeViewModel.Email,
                Name = employeeViewModel.Name,
                PhotoURL = employeeViewModel.PhotoURL,
                Sex = employeeViewModel.Sex,
                MaritalStatus = employeeViewModel.MaritalStatus,
                DepartmentId = employeeViewModel.DepartmentId,
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public void DeleteEmployee(EmployeeViewModel employee)
        {
            //if (employee != null)
            //{
            //    _context.Employees.Remove(employee);
            //    _context.SaveChanges();
            //}
            //else
            //{
            //    throw new Exception("The Value was not found ");
            //}
            throw new NotImplementedException();
        }

        public async Task<List<Employee>> GetAll()
        {
            var retLis = await _context.Employees.ToListAsync();
            return retLis;
        }

        public async Task<Employee> GetById(int id)
        {
            var retval = await _context.Employees.Include(e => e.Department).FirstOrDefaultAsync(n => n.Id ==id);
            if (retval != null)
            {
                return retval;
            }
            else
            {
                throw new Exception($"The Value was not found with an id {id}");
            }
        }

        public async Task UpdateEmployee(EmployeeViewModel employee)
        {
            var DbEmployee = await _context.Employees.FirstOrDefaultAsync(n => n.Id == employee.Id);
            if (DbEmployee != null)
            {
                DbEmployee.Name = employee.Name;
                DbEmployee.Email = employee.Email;
                DbEmployee.PhotoURL = employee.PhotoURL;
                DbEmployee.DepartmentId = employee.DepartmentId;
                DbEmployee.MaritalStatus = employee.MaritalStatus;
                DbEmployee.Sex = employee.Sex;

            }
        }
        public async Task<DepartmentdropdownViewModel> GetDepartmentdropdowns()
        {
            var response = new DepartmentdropdownViewModel()
            {
                Departments = await _context.Departments.ToListAsync()
            };
            return response;
        }

       
    }
}
