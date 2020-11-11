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
using ICourses.Data.Interfaces;
using ICourses.ViewModel;

namespace ICourses.Controllers
{
    [Authorize]
    public class TextMaterialsController : Controller
    {
        private readonly CourseDbContext _context;
        private readonly ITextMaterial _text;

        public TextMaterialsController(CourseDbContext context, ITextMaterial text)
        {
            _text = text;
            _context = context;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            var appDbContext = _context.TextMaterials.Where(t => t.Module.Id == id);
            return View(await appDbContext.ToListAsync());
        }

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

        [Authorize(Roles = "admin,teacher")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,teacher")]
        public async Task<IActionResult> Create(Guid id, [Bind("Id,Name,Context")] TextMaterial textMaterial)
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
            //ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Id", textMaterial.ModuleId);
            return View(textMaterial);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> Edit(Guid id, ChangeTextViewModel text)
        {
            if (ModelState.IsValid)
            {
                TextMaterial new_text = await _text.GetTextMaterial(id);

                new_text.Name = text.Name;
                new_text.Context = text.Context;

                await _text.UpdateTextMaterial(new_text);
                return RedirectToAction("Details", "Modules", new { id = new_text.ModuleId });
            }
            return View(text);
        }

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
