using Microsoft.AspNetCore.Mvc;

namespace Human_Resources.Controllers
{
    public class PositionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
