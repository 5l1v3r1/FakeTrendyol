using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mozambik.Data;

namespace MozambikMVC.Areas.Jungle.Controllers
{
    [Area("Jungle")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private ProductDbContext _dbContext;
        private UserManager<AppUser> _manager;
        public OrderController(ProductDbContext dbContext,
                               UserManager<AppUser> userManager)
        {
            _manager = userManager;
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View (_dbContext.Orders.Include(x => x.Product).Include(x => x.User).ToList());
        }
        public IActionResult Canceled()
        {
            return View(_dbContext.Orders.Include(x => x.Product).Include(x => x.User).Where(x=>x.isCanceled).ToList());
        }
        public IActionResult Delivered()
        {
            return View(_dbContext.Orders.Include(x => x.Product).Include(x => x.User).Where(x => x.isDelivered).ToList());
        }
        [HttpPost]
        public IActionResult Deliver(int id)
        {
            var data = _dbContext.Orders.Find(id);
            if (data != null)
            {
                 data.isDelivered = true;
                data.DeliveredDate = DateTime.Now;
                _dbContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public IActionResult NotDelivered()
        {
            return View(_dbContext.Orders.Include(x => x.Product).Include(x => x.User).Where(x => !x.isDelivered).ToList());
        }
        [HttpPost]
        public IActionResult Details(int id)
        {
            return View(_dbContext.Orders.Include(x => x.Product)
                                        .ThenInclude(x=>x.Model)
                                        .ThenInclude(x=>x.Marka)
                                        .Include(x => x.User)
                                        .FirstOrDefault(x => x.Id == id));
        }
    }
}