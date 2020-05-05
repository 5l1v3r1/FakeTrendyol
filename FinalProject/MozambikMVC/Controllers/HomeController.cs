using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mozambik.Data;
using MozambikMVC.Models;

namespace MozambikMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductDbContext dbContext;
        public HomeController(ProductDbContext product)
        {
            dbContext = product;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult PartialView(LocalStorageModel[] LSModel)
        {
            return PartialView("LocalStoragePartial", LSModel);
        }

       
    }
}