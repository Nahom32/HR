﻿using Microsoft.AspNetCore.Mvc;

namespace Human_Resources.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
