using Microsoft.AspNetCore.Mvc;

namespace Human_Resources.Controllers
{
    public class LeaveController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
