﻿using Microsoft.AspNetCore.Mvc;

namespace Coza_Ecommerce_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
       
        public IActionResult Index()
        {
            
            return View();
        }
    }
}
