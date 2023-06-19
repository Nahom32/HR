using Human_Resources.Data.Services;
using Human_Resources.Data.ViewModels;
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
                var certContainer = new CertificationListViewModel();
                certContainer.Certifications = certificates;
                certContainer.EmployeeId = FindUser.EmployeeId;
                return View(certContainer);
            }
        }
        [HttpGet]
        [Authorize(Roles ="User")]
        public async Task<IActionResult> AddCertification()
        {
            var User = _contextAccessor.HttpContext.User;
            var FindUser = await _userManager.GetUserAsync(User);
            if (FindUser != null)
            {
                Certification certificate = new Certification();
                certificate.EmployeeId = FindUser.EmployeeId;
                return PartialView("~/Views/Shared/_AddCertification",certificate);
            }
            else
            {
                return NotFound();
            }

        }
        [HttpPost]
        public async Task<IActionResult> AddCertification(Certification certification)
        {
            var User = _contextAccessor.HttpContext.User;
            var FindUser = await _userManager.GetUserAsync(User);
            if (FindUser != null)
            {
                
                certification.EmployeeId = FindUser.EmployeeId;
            }
                if (ModelState.IsValid)
            {
                await _service.AddCertification(certification);
                return RedirectToAction("GetCertificates");
            }
            else
            {
                return PartialView("~/Views/Shared/_AddCertification.cshtml", certification);
            }
        }
        [HttpGet]
        public async Task<IActionResult> EditCertification(int CertificationId)
        {
            var data = await _service.GetById(CertificationId);
            if(data != null)
            {
                return PartialView("~/Views/Shared/_EditCertification.cshtml", data);
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
                return PartialView("~/Views/Shared/_EditCertification.cshtml", certification);
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteCertification(int CertificationId)
        {
            var data = await _service.GetById(CertificationId);
            if (data != null)
            {
                return PartialView("~/Views/Shared/_EditCertification.cshtml", data);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost, ActionName("DeleteCertificate")]
        public async Task<IActionResult> DeleteCertificationConfirmed(int certificationId)
        {
            var certification = await _service.GetById(certificationId);
            if (certification != null)
            {
                await _service.DeleteCertification(certification);
                return RedirectToAction("GetCertificate");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult>FindCertificates()
        {
            var User = _contextAccessor.HttpContext.User;
            var AppUser = await _userManager.GetUserAsync(User);
            var certificates = await _service.FindByEmployeeId(AppUser.EmployeeId);
            if(certificates == null)
            {
                return View(new List<Certification>());
            }
            return View(certificates);
        }

    }
}
