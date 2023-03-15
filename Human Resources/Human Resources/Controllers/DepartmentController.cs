using Microsoft.AspNetCore.Mvc;

namespace Human_Resources.Controllers
{
    public class DepartmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
