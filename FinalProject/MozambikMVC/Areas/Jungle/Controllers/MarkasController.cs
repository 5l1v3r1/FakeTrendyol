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
    public class MarkasController : Controller
    {
        private readonly ProductDbContext _context;

        public MarkasController(ProductDbContext context)
        {
            _context = context;
        }

        // GET: Jungle/Markas
        public async Task<IActionResult> Index()
        {
            var productDbContext = _context.Markas.Include(m => m.Category);
            return View(await productDbContext.ToListAsync());
        }

        // GET: Jungle/Markas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marka = await _context.Markas
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marka == null)
            {
                return NotFound();
            }

            return View(marka);
        }

        // GET: Jungle/Markas/Create
        public IActionResult Create()
        {
            ViewBag.Menu = new SelectList(_context.Menus, "ID", "Name");
            return View();
        }

        // POST: Jungle/Markas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CategoryId")] Marka marka)
        {
            if (ModelState.IsValid)
            {
                _context.Add(marka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Menu = new SelectList(_context.Menus, "ID", "Name");
            return View(marka);
        }

        // GET: Jungle/Markas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marka = await _context.Markas.FindAsync(id);
            if (marka == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", marka.CategoryId);
            return View(marka);
        }

        // POST: Jungle/Markas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CategoryId")] Marka marka)
        {
            if (id != marka.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(marka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarkaExists(marka.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", marka.CategoryId);
            return View(marka);
        }

        // GET: Jungle/Markas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marka = await _context.Markas
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marka == null)
            {
                return NotFound();
            }

            return View(marka);
        }

        // POST: Jungle/Markas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var marka = await _context.Markas.FindAsync(id);
            _context.Markas.Remove(marka);
            await _context.SaveChangesAsync();
            DeletedMarka deleted = new DeletedMarka
            {
                DeletedDate = DateTime.Today,
                DeletedId = marka.Id,
                Name = marka.Name
            };
            _context.DeletedMarkas.Add(deleted);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool MarkaExists(int id)
        {
            return _context.Markas.Any(e => e.Id == id);
        }
    }
}
