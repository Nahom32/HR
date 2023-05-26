using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Human_Resources.Controllers
{
    [Authorize(Roles ="Admin,User,HRManager")]
    public class DisciplineController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
