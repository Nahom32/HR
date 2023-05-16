using Microsoft.AspNetCore.Mvc;

namespace Human_Resources.Controllers
{
    public class DisciplineController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
