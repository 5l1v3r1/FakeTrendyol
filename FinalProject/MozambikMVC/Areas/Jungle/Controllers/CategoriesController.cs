using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mozambik.Data;
using MozambikMVC.Data.DeletedEtities;
using MozambikMVC.Data.Entities;

namespace MozambikMVC.Areas.Jungle.Controllers
{
    [Area("Jungle")]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly ProductDbContext _context;

        public CategoriesController(ProductDbContext context)
        {
            _context = context;
        }

        // GET: Jungle/Categories
        public async Task<IActionResult> Index()
        {
            var productDbContext = _context.Category.Include(c => c.SubMenu);
            return View(await productDbContext.ToListAsync());
        }

        // GET: Jungle/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .Include(c => c.SubMenu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Jungle/Categories/Create
        public IActionResult Create()
        {
            ViewBag.Menu = new SelectList(_context.Menus, "ID", "Name");
            return View();
        }

        // POST: Jungle/Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,SubMenuID")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Menu = new SelectList(_context.Menus, "ID", "Name");
            return View(category);
        }

        // GET: Jungle/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            ViewData["SubMenuID"] = new SelectList(_context.SubMenus, "Id", "Name", category.SubMenuID);
            return View(category);
        }

        // POST: Jungle/Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,SubMenuID")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubMenuID"] = new SelectList(_context.SubMenus, "ID", "Name", category.SubMenuID);
            return View(category);
        }

        // GET: Jungle/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .Include(c => c.SubMenu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Jungle/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Category.FindAsync(id);
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
            DeletedCategory deleted = new DeletedCategory
            {
                DeletedDate = DateTime.Today,
                DeletedId = category.Id,
                Name = category.Name
            };
            _context.DeletedCategories.Add(deleted);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.Id == id);
        }
        [Route("/GetSubMenus")]
        [HttpPost]
        public List<SubMenu> GetSubMenus(int id)
        => _context.SubMenus.Where(x => x.MenuId == id).ToList();

        [Route("/GetCategories")]
        [HttpPost]
        public List<Category> GetCategories(int id)
        => _context.Category.Where(x => x.SubMenuID == id).ToList();

        [Route("/GetMarkas")]
        [HttpPost]
        public List<Marka> GetMarkas(int id)
        => _context.Markas.Where(x => x.CategoryId == id).ToList();

        [Route("/GetModels")]
        [HttpPost]
        public List<Model> GetModels(int id)
        => _context.Models.Where(x => x.MarkaID == id).ToList();
    }
}
