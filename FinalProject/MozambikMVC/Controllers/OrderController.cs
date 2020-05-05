using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mozambik.Data;
using MozambikMVC.Data.Entities;
using MozambikMVC.Models;

namespace MozambikMVC.Controllers
{
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
            return View();
        }
        [HttpPost]
        public bool Buy(LocalStorageModel[] LSModel)
        {
            try
            {
                if (LSModel.Length == 0)
                {
                    return false;
                }
                foreach (var model in LSModel)
                {
                    var oreder= new Order
                    {
                        Count = model.Quantity,
                        Price = (decimal)model.Price,
                        UserID = _manager.FindByNameAsync(HttpContext.User.Identity.Name).Result.Id,
                        OrderDate = DateTime.Now,
                        ProductId = model.Id,
                    };
                    _dbContext.Orders.Add(oreder) ;
                    _dbContext.SaveChanges();
                    
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    

        public IActionResult Remove(int id)
        {
            try
            {
                var data = _dbContext.Orders.Find(id);
                if (data != null)
                {
                    data.isCanceled = true;
                    data.CanceledDate = DateTime.Now;
                    _dbContext.SaveChanges();
                }
            }
            catch 
            {

            }
            return RedirectToAction("OrdersList", "Product", new { Area = "null" });
        }
    }
}