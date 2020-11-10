using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ICourses.Data;
using ICourses.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace ICourses.Controllers
{
    [Authorize]
    public class TextMaterialsController : Controller
    {
        private readonly CourseDbContext _context;

        public TextMaterialsController(CourseDbContext context)
        {
            _context = context;
        }

        // GET: TextMaterials
        public async Task<IActionResult> Index(Guid id)
        {
            var appDbContext = _context.TextMaterials.Where(t => t.Module.Id == id);
            return View(await appDbContext.ToListAsync());
        }

        // GET: TextMaterials/Details/5
        public async Task<IActionResult> Details(Guid? id)
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
        [Authorize(Roles = "admin,teacher")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: TextMaterials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,teacher")]
        public async Task<IActionResult> Create(Guid id, [Bind("Id,Name,Context,ModuleId")] TextMaterial textMaterial)
        {
           
            if (ModelState.IsValid)
            {
                textMaterial.Id = Guid.NewGuid();
                textMaterial.ModuleId = _context.Modules.FirstOrDefault(_ => _.Id == id).Id;

                _context.Add(textMaterial);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Modules", new { id = textMaterial.ModuleId });
            }

            return View(textMaterial);
        }

        // GET: TextMaterials/Edit/5
        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> Edit(Guid? id)
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
        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Context,ModuleId")] TextMaterial textMaterial)
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
                return RedirectToAction("Details", "Modules", new { id = textMaterial.ModuleId });
            }
            ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Id", textMaterial.ModuleId);
            return View(textMaterial);
        }

        // GET: TextMaterials/Delete/5
        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> Delete(Guid? id)
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
        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var textMaterial = await _context.TextMaterials.FindAsync(id);
            _context.TextMaterials.Remove(textMaterial);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Modules", new { id = textMaterial.ModuleId });
        }

        private bool TextMaterialExists(Guid id)
        {
            return _context.TextMaterials.Any(e => e.Id == id);
        }
    }
}
