using Human_Resources.Data.Services;
using Human_Resources.Data.ViewModels;
using Human_Resources.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Human_Resources.Controllers
{
    public class RewardController : Controller
    {
        private readonly IRewardService _service;
        private readonly ILogger<RewardController> _logger;
        public RewardController(IRewardService service, ILogger<RewardController> logger)
        {
            _service = service;

            _logger = logger;

        }
        public async Task<IActionResult>Index()
        {
            var Rewards = await _service.GetAll();
            return View(Rewards);
        }
        [HttpGet]
        public async Task<IActionResult> AddReward()
        {
            var Employees = await _service.GetEmployeedropdowns();
            ViewBag.Employees = new SelectList(Employees.Employees, "Id", "Name");
            RewardViewModel rewardVm = new RewardViewModel();

            return View(rewardVm);
        }
        [HttpPost]
        public async Task<IActionResult> AddReward(RewardViewModel reward)
        {
            if(!ModelState.IsValid)
            {
                _logger.LogInformation("Fail");
                var Employees = await _service.GetEmployeedropdowns();
                ViewBag.Employees = new SelectList(Employees.Employees, "Id", "Name");
               
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    _logger.LogError(error.ErrorMessage);
                }
                return View(reward);
            }
            else
            {
                await _service.AddReward(reward);
                return RedirectToAction("Index", "Reward");
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditReward(int id)
        {
            var reward = await _service.GetById(id);
            if (reward != null)
            {
                RewardViewModel rewardVm = new RewardViewModel()
                {
                    Id = reward.Id,
                    DateTime = reward.DateTime,
                    Amount = reward.Amount,
                    EmployeeId = reward.EmployeeId,
                    Reason = reward.Reason,
                    
                };
                var Employees = await _service.GetEmployeedropdowns();
                ViewBag.Employees = new SelectList(Employees.Employees, "Id", "Name");
                return View(rewardVm);
            }
            else
            {
                return View("The reward entry doesn't exist");
            }

        }
        [HttpPost]
        public async Task<IActionResult> EditReward(RewardViewModel reward)
        {
            if(!ModelState.IsValid)
            {
                var Employees = await _service.GetEmployeedropdowns();
                ViewBag.Employees = new SelectList(Employees.Employees, "Id", "Name");
                return View(reward);

            }
            else 
            {
                await _service.UpdateReward(reward);
                return RedirectToAction("Index","Reward");
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteReward(int id)
        {
            var reward = await _service.GetById(id);
            if (reward != null)
            {
                RewardViewModel rewardVm = new RewardViewModel()
                {
                    Id = reward.Id,
                    DateTime = reward.DateTime,
                    Amount = reward.Amount,
                    EmployeeId = reward.EmployeeId,
                    Reason = reward.Reason,

                };
                var Employees = await _service.GetEmployeedropdowns();
                ViewBag.Employees = new SelectList(Employees.Employees, "Id", "Name");
                return View(rewardVm);
            }
            else
            {
                return View("The reward entry doesn't exist");
            }

        }
        [HttpPost,ActionName("DeleteReward")]
        public async Task<IActionResult> DeleteRewardConfirmed(int id)
        {
            var reward = await _service.GetById(id);
            if (reward != null)
            {
                RewardViewModel rewardVm = new RewardViewModel()
                {
                    Id = reward.Id,
                    DateTime = reward.DateTime,
                    Amount = reward.Amount,
                    EmployeeId = reward.EmployeeId,
                    Reason = reward.Reason,

                };
                await _service.DeleteReward(rewardVm);
                return RedirectToAction("Index", "Reward");
            }
            else
            {
                return View("The following reward doesn't exist");

            }



        }


    }
}
