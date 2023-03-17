using Human_Resources.Data.Services;
using Human_Resources.Models;
using Microsoft.AspNetCore.Mvc;

namespace Human_Resources.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _service;
        public EmployeeController(IEmployeeService service)
        {
            _service = service;   
        }
        public async  Task<IActionResult> Index()
        {
            var Employees = await _service.GetAll();
            return View(Employees);
        }
        public IActionResult AddEmployee()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            

        }
    }
}
