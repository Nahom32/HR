using Human_Resources.Data.Services;
using Human_Resources.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Human_Resources.Controllers
{
    [Authorize]
    public class CertificationController : Controller
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly ICertificationService _service;
        private readonly UserManager<ApplicationUser> _userManager;
        public CertificationController(IHttpContextAccessor contextAccessor,
            ICertificationService service,
            UserManager<ApplicationUser>userManager)
        {
            _contextAccessor = contextAccessor;
            _service = service;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        
        public async Task<IActionResult> GetCertificates()
        {
            var User = _contextAccessor.HttpContext.User;
            var FindUser = await _userManager.GetUserAsync(User);
            if (FindUser == null)
            {
                return NotFound();
            }
            else
            {
                var certificates = await _service.FindByEmployeeId(FindUser.EmployeeId);
                return View(certificates);
            }
        }
        [HttpGet]
        public async Task<IActionResult> AddCertification()
        {
            var User = _contextAccessor.HttpContext.User;
            var FindUser = await _userManager.GetUserAsync(User);
            if (FindUser != null)
            {
                Certification certificate = new Certification();
                certificate.EmployeeId = FindUser.EmployeeId;
                return View(certificate);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpPost]
        public async Task<IActionResult> AddCertification(Certification certification)
        {
            if (ModelState.IsValid)
            {
                await _service.AddCertification(certification);
                return RedirectToAction("GetCertificates");
            }
            else
            {
                return View(certification);
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditCertification(int CertificationId)
        {
            var data = await _service.GetById(CertificationId);
            if(data != null)
            {
                return View(data);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> EditCertification(Certification certification)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateCertification(certification);
                return RedirectToAction("GetCertificate");
            }
            else
            {
                return View(certification);
            }
        }

    }
}
