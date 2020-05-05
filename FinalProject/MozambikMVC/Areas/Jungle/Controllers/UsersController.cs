using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mozambik.Data;

namespace MozambikMVC.Areas.Jungle.Controllers
{
    [Area("Jungle")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly ProductDbContext _context;
        public UsersController(ProductDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View(_context.Users.ToList());
        }
    }
}