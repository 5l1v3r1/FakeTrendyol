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
    public class ProductController : Controller
    {
        private ProductDbContext _dbContext;
        private UserManager<AppUser> _manager;
        public ProductController(ProductDbContext dbContext,
                               UserManager<AppUser> userManager)
        {
            _manager = userManager;
            _dbContext = dbContext;
        }
        public IActionResult Index(int id)
        {
            return View(_dbContext.Category.Include(x=>x.Markas).FirstOrDefaultAsync(x=>x.Id==id).Result);
        }
        public IActionResult OrdersList()
        {
            return View(_dbContext.Orders.
                Include(x=>x.Product).
                ThenInclude(x=>x.Model).
                ThenInclude(x=>x.Marka).
                Where(x=>x.UserID == _manager.FindByNameAsync(HttpContext.User.Identity.Name).Result.Id && !x.isDelivered&&!x.isCanceled));
        }


        [HttpPost]
        public IActionResult PartialViewCaller(int data, int id) {

            if (data == 0)
            {

              return  PartialView("ProductPartial2", _dbContext.Products
                       .Include(x => x.Model)
                       .ThenInclude(x => x.Marka)
                       .ThenInclude(x => x.Category)
                       .Include(x => x.Pictures)
                       .Where(x => x.Model.Marka.Category.Id == id && x.Price >= data && x.Price <= 100).Take(9).ToList());
            }else if(data ==100){
                return PartialView("ProductPartial2", _dbContext.Products
                      .Include(x => x.Model)
                      .ThenInclude(x => x.Marka)
                      .ThenInclude(x => x.Category)
                      .Include(x => x.Pictures)
                      .Where(x => x.Model.Marka.Category.Id == id && x.Price >= data && x.Price <= 500).Take(9).ToList());
            }
            else if (data == 500)
            {
                return PartialView("ProductPartial2", _dbContext.Products
                      .Include(x => x.Model)
                      .ThenInclude(x => x.Marka)
                      .ThenInclude(x => x.Category)
                      .Include(x => x.Pictures)
                      .Where(x => x.Model.Marka.Category.Id == id && x.Price >= data && x.Price <= 10000).Take(9).ToList());
            }
            else if (data == 10000)
            {
                return PartialView("ProductPartial2", _dbContext.Products
                      .Include(x => x.Model)
                      .ThenInclude(x => x.Marka)
                      .ThenInclude(x => x.Category)
                      .Include(x => x.Pictures)
                      .Where(x => x.Model.Marka.Category.Id == id && x.Price >= data && x.Price <= 20000).Take(9).ToList());
            }
            else
            {
                return PartialView("ProductPartial2", _dbContext.Products
                     .Include(x => x.Model)
                     .ThenInclude(x => x.Marka)
                     .ThenInclude(x => x.Category)
                     .Include(x => x.Pictures)
                     .Where(x => x.Model.Marka.Category.Id == id && x.Price >= 20000).Take(9).ToList());
            }

        }
         [HttpPost]
        public IActionResult PartialViewCaller2(int marka,int id) =>
            PartialView("ProductPartial2", _dbContext.Products
                   .Include(x => x.Model)
                   .ThenInclude(x => x.Marka)
                   .ThenInclude(x => x.Category)
                   .Include(x => x.Pictures)
                   .Where(x => x.Model.Marka.Category.Id ==id && x.Model.Marka.Id ==marka).Take(9).ToList()); 
        [HttpPost]
        public IActionResult PartialViewCaller3(int number, int id) =>
           
            PartialView("ProductPartial2", _dbContext.Products
                   .Include(x => x.Model)
                   .ThenInclude(x => x.Marka)
                   .ThenInclude(x => x.Category)
                   .Include(x => x.Pictures)
                   .Where(x => x.Model.Marka.Category.Id ==id).Skip((number-1)*9).Take(9).ToList());
        
        public IActionResult Details(int id)
         => View(_dbContext.Products.Include(x => x.Properties).
                            Include(x => x.Model).ThenInclude(x => x.Marka).ThenInclude(x=>x.Category).
                            Include(x=>x.Pictures).FirstOrDefault(x=>x.Id==id));
        [HttpPost]
        public IActionResult Search(string Search)
            =>View("SearchResult",_dbContext
                .Products.Include(x => x.Model).ThenInclude(x => x.Marka)
                .Where(x => x.Name.ToLower().Contains(Search.ToLower())||
                x.Model.Name.ToLower().Contains(Search.ToLower())||
                x.Model.Marka.Name.ToLower().Contains(Search.ToLower()))
                .Include(x=>x.Pictures)
                .ToList());
        
        //public IActionResult SearchResult(List<Product> products)
        //{
        //    return View(products);
        //}

    }
}