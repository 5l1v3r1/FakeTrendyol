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
    public class SubMenusController : Controller
    {
        private readonly ProductDbContext _context;

        public SubMenusController(ProductDbContext context)
        {
            _context = context;
        }

        // GET: Jungle/SubMenus
        public async Task<IActionResult> Index()
        {
            var productDbContext = _context.SubMenus.Include(s => s.Menu);
            return View(await productDbContext.ToListAsync());
        }

        // GET: Jungle/SubMenus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subMenu = await _context.SubMenus
                .Include(s => s.Menu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subMenu == null)
            {
                return NotFound();
            }

            return View(subMenu);
        }

        // GET: Jungle/SubMenus/Create
        public IActionResult Create()
        {
            ViewData["MenuId"] = new SelectList(_context.Menus, "ID", "Name");
            return View();
        }

        // POST: Jungle/SubMenus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MenuId")] SubMenu subMenu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subMenu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MenuId"] = new SelectList(_context.Menus, "ID", "Name", subMenu.MenuId);
            return View(subMenu);
        }

        // GET: Jungle/SubMenus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subMenu = await _context.SubMenus.FindAsync(id);
            if (subMenu == null)
            {
                return NotFound();
            }
            ViewData["MenuId"] = new SelectList(_context.Menus, "ID", "Name", subMenu.MenuId);
            return View(subMenu);
        }

        // POST: Jungle/SubMenus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,MenuId")] SubMenu subMenu)
        {
            if (id != subMenu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subMenu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubMenuExists(subMenu.Id))
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
            ViewData["MenuId"] = new SelectList(_context.Menus, "ID", "Name", subMenu.MenuId);
            return View(subMenu);
        }

        // GET: Jungle/SubMenus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subMenu = await _context.SubMenus
                .Include(s => s.Menu)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (subMenu == null)
            {
                return NotFound();
            }

            return View(subMenu);
        }

        // POST: Jungle/SubMenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subMenu = await _context.SubMenus.FindAsync(id);
            _context.SubMenus.Remove(subMenu);
            await _context.SaveChangesAsync();
            DeletedSubMenu deleted = new DeletedSubMenu
            {
                DeletedId = subMenu.Id,
                Name = subMenu.Name
            };
            _context.DeletedSubMenus.Add(deleted);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool SubMenuExists(int id)
        {
            return _context.SubMenus.Any(e => e.Id == id);
        }
    }
}
