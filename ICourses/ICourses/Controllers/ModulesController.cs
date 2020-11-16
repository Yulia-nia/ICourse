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
using Microsoft.AspNetCore.Identity;
using ICourses.Data.Interfaces;
using System.IO;
using ICourses.ViewModel;

namespace ICourses.Controllers
{
    [Authorize]
    public class ModulesController : Controller
    {
        readonly UserManager<User> _userManager;
        private readonly CourseDbContext _context;
        private readonly IModule _module;


        public ModulesController(CourseDbContext context, UserManager<User> userManager, IModule module)
        {
            _module = module;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules
                .Include(_ => _.Course)
                .Include(v => v.Videos)
                .Include(t => t.TextMaterials)
                .FirstOrDefaultAsync(m => m.Id == id);

            ViewBag.Texts = _context.TextMaterials.Where(_ => _.ModuleId == id).ToList();

            ViewBag.Videos = _context.Videos.Where(_ => _.Moduleid == id).ToList();

            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }


        [Authorize(Roles = "admin,teacher")]
        public IActionResult Create(Guid? id)
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,teacher")]
        public async Task<IActionResult> Create(Guid id, CreateModuleViewModel module)
        {
            byte[] imageData = null;

            using (var binaryReader = new BinaryReader(module.Image.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)module.Image.Length);
            }

            if (ModelState.IsValid)
            {
                Module new_module = new Module
                {
                    Id = Guid.NewGuid(),
                    Name = module.Name,
                    Description = module.Description,
                    Image = imageData,
                    Modified = DateTime.Now,
                    CourseId = _context.Courses.FirstOrDefault(_ => _.Id == id).Id,
                };
                await _module.AddModule(new_module);              
                return RedirectToAction("Details", "Courses", new { id = new_module.CourseId });
            }            
            return View(@module);
        }



        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules.FindAsync(id);
            if (@module == null)
            {
                return NotFound();
            }
            return View(@module);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> Edit(Guid id, ChangeModuleViewModel module)
        {         
            if (ModelState.IsValid)
            {

                byte[] imageData = null;

                using (var binaryReader = new BinaryReader(module.Image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)module.Image.Length);
                }

                Module new_module = await _module.GetModule(id);

                if (new_module != null)
                {
                    new_module.Name = module.Name;
                    new_module.Description = module.Description;
                    new_module.Image = imageData;
                    await _module.UpdateModule(new_module);
                    return RedirectToAction("Details", "Modules", new { id = id });
                }
            }            
            return View(@module);
        }

        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await _context.Modules
                .Include(_ => _.Course)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@module == null)
            {
                return NotFound();
            }

            return View(@module);
        }
        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var module = await _context.Modules.FindAsync(id);
            await _module.DeleteModuleById(module.Id);
            //_context.Modules.Remove(@module);
            //await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Courses", new { id = module.CourseId });
        }

        private bool ModuleExists(Guid id)
        {
            return _context.Modules.Any(e => e.Id == id);
        }
    }
}
