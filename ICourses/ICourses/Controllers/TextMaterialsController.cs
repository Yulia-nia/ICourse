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
using ICourses.Services.Interfaces;

namespace ICourses.Controllers
{
    [Authorize]
    public class TextMaterialsController : Controller
    {
        ITextService _textService;

        public TextMaterialsController(ITextService textService)
        {
            _textService = textService;
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var textMaterial = await _textService.GetTextMaterial(id);

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
        public async Task<IActionResult> Create(Guid id, TextViewModel text)
        {           
            if (ModelState.IsValid)
            {
                TextMaterial textMaterial = await _textService.AddTextMaterial(id, text);    
                if(textMaterial != null)      
                    return RedirectToAction("Details", "Modules", new { id = textMaterial.ModuleId });
            }
            return View(text);
        }

        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var textMaterial = await _textService.GetTextMaterial(id);;
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
        public async Task<IActionResult> Edit(Guid id, TextViewModel text)
        {
            if (ModelState.IsValid)
            {
                TextMaterial new_text = await _textService.EditTextMaterial(id, text);
                if (new_text != null)
                    return RedirectToAction("Details", "Modules", new { id = new_text.ModuleId });
            }
            return View(text);
        }

        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var textMaterial = await _textService.GetTextMaterial(id);
                
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
            var textMaterial = await _textService.GetTextMaterial(id);
            await _textService.DeleteTextMaterialById(id);
            return RedirectToAction("Details", "Modules", new { id = textMaterial.ModuleId });
        }
    }
}
