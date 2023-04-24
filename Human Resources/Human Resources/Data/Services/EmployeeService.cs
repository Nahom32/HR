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
                PhotoURL = employeeViewModel.PhotoURL.FileName,
                Sex = employeeViewModel.Sex,
                MaritalStatus = employeeViewModel.MaritalStatus,
                DepartmentId = employeeViewModel.DepartmentId,
                PositionId = employeeViewModel.PositionId,
                EducationalLevel = employeeViewModel.EducationalLevel,
                EducationalFieldId = employeeViewModel.EducationalFieldId,
                Roles = employeeViewModel.Roles
            };
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployee(EmployeeViewModel employee)
        {
            var toDelete  = await _context.Employees.FirstOrDefaultAsync(n => n.Id == employee.Id);
            if (toDelete != null)
            {
                _context.Employees.Remove(toDelete);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("The described Employee doesn't Exist");
            }
        
        }

        public async Task<List<Employee>> GetAll()
        {
            var retLis = await _context.Employees.Include(n => n.Department)
                .Include(n => n.Position)
                .Include(n => n.EducationalField)
                .ToListAsync();
            return retLis;
        }

        public async Task<Employee> GetById(int id)
        {
            var retval = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Position)
                .Include(e => e.EducationalField)
                .FirstOrDefaultAsync(n => n.Id ==id);
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
                DbEmployee.PhotoURL =employee.PhotoURL.FileName;
                DbEmployee.DepartmentId = employee.DepartmentId;
                DbEmployee.MaritalStatus = employee.MaritalStatus;
                DbEmployee.Sex = employee.Sex;
                DbEmployee.EducationalFieldId = employee.EducationalFieldId;
                DbEmployee.EducationalLevel = employee.EducationalLevel;
                DbEmployee.PositionId = employee.PositionId;
                DbEmployee.Roles = employee.Roles;
                _context.Employees.Update(DbEmployee);
                _context.SaveChanges();
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
        public async Task<PositiondropdownViewModel> GetPositiondropdowns()
        {
            var response = new PositiondropdownViewModel()
            {
                Positions = await _context.Positions.ToListAsync()
        };
            return response;
        }
        public async Task<EducationalFielddropdownViewModel> GetEducationalFielddropdowns()
        {
            var response = new EducationalFielddropdownViewModel()
            {
                EducationalFields = await _context.EducationalFields.ToListAsync()
            };
            return response;
        }
        public async Task<Employee> GetEmployeeByEmail(string email)
        {
            var employee = await _context.Employees.FirstOrDefaultAsync(n => n.Email == email);
            if(employee != null)
            {
                return employee;
            }
            else
            {
                throw new Exception($"A user with {email} doesn't exist");
            }
        }


    }
}
