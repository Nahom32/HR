using Human_Resources.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Human_Resources.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _service;
        public DepartmentController(IDepartmentService service)
        {
            _service = service;  
        }
        [HttpGet]
        public async  Task<IActionResult> Index()
        {
            var allBranches = await _service.GetAll();

            return View(allBranches);
        }

    }
}
