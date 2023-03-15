using Microsoft.AspNetCore.Mvc;

namespace Human_Resources.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
