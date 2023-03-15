using Microsoft.AspNetCore.Mvc;

namespace Human_Resources.Controllers
{
    public class BranchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
