using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ICourses.Data;
using ICourses.Data.Models;

namespace ICourses.Controllers
{
    public class TextMaterialsController : Controller
    {
        private readonly AppDbContext _context;

        public TextMaterialsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: TextMaterials
        public async Task<IActionResult> Index(int id)
        {
            var appDbContext = _context.TextMaterials.Where(t => t.Module.Id == id);
            return View(await appDbContext.ToListAsync());
        }

        // GET: TextMaterials/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var textMaterial = await _context.TextMaterials
                .Include(t => t.Module)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (textMaterial == null)
            {
                return NotFound();
            }

            return View(textMaterial);
        }

        // GET: TextMaterials/Create
        public IActionResult Create()
        {
            ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Id");
            return View();
        }

        // POST: TextMaterials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Context,ModuleId")] TextMaterial textMaterial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(textMaterial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Id", textMaterial.ModuleId);
            return View(textMaterial);
        }

        // GET: TextMaterials/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var textMaterial = await _context.TextMaterials.FindAsync(id);
            if (textMaterial == null)
            {
                return NotFound();
            }
            ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Id", textMaterial.ModuleId);
            return View(textMaterial);
        }

        // POST: TextMaterials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Context,ModuleId")] TextMaterial textMaterial)
        {
            if (id != textMaterial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(textMaterial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TextMaterialExists(textMaterial.Id))
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
            ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Id", textMaterial.ModuleId);
            return View(textMaterial);
        }

        // GET: TextMaterials/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var textMaterial = await _context.TextMaterials
                .Include(t => t.Module)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (textMaterial == null)
            {
                return NotFound();
            }

            return View(textMaterial);
        }

        // POST: TextMaterials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var textMaterial = await _context.TextMaterials.FindAsync(id);
            _context.TextMaterials.Remove(textMaterial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TextMaterialExists(int id)
        {
            return _context.TextMaterials.Any(e => e.Id == id);
        }
    }
}
