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
    public class ModelsController : Controller
    {
        private readonly ProductDbContext _context;

        public ModelsController(ProductDbContext context)
        {
            _context = context;
        }

        // GET: Jungle/Models
        public async Task<IActionResult> Index()
        {
            var productDbContext = _context.Models.Include(m => m.Marka);
            return View(await productDbContext.ToListAsync());
        }

        // GET: Jungle/Models/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Models
                .Include(m => m.Marka)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // GET: Jungle/Models/Create
        public IActionResult Create()
        {
            ViewBag.Menu = new SelectList(_context.Menus, "ID", "Name");
            return View();
        }

        // POST: Jungle/Models/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,MarkaID")] Model model)
        {
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Menu = new SelectList(_context.Menus, "ID", "Name");
            return View(model);
        }

        // GET: Jungle/Models/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Models.FindAsync(id);
            
            if (model == null)
            {
                return NotFound();
            }
            ViewData["MarkaID"] = new SelectList(_context.Markas, "Id", "Name", model.MarkaID);
            return View(model);
        }

        // POST: Jungle/Models/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,MarkaID")] Model model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(model);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModelExists(model.Id))
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
            ViewData["MarkaID"] = new SelectList(_context.Markas, "Id", "Name", model.MarkaID);
            return View(model);
        }

        // GET: Jungle/Models/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.Models
                .Include(m => m.Marka)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST: Jungle/Models/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var model = await _context.Models.FindAsync(id);
            _context.Models.Remove(model);
            await _context.SaveChangesAsync();
            DeletedModel deleted = new DeletedModel
            {
                Name = model.Name
            };
            _context.DeletedModels.Add(deleted);
            return RedirectToAction(nameof(Index));
        }

        private bool ModelExists(int id)
        {
            return _context.Models.Any(e => e.Id == id);
        }
    }
}
