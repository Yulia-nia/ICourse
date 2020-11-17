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
using ICourses.Services.Interfaces;

namespace ICourses.Controllers
{
    [Authorize]
    public class ModulesController : Controller
    {       
         IModuleService _moduleService;
        ITextService _textService;
        IVideoService _videoService;
        //private readonly CourseDbContext _context;

        public ModulesController(IModuleService moduleService, IVideoService videoService, ITextService textService)
        {
            _videoService = videoService;
            _textService = textService;            
            _moduleService = moduleService;
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Module module = await _moduleService.GetModule(id);
            ViewBag.Texts = await _textService.GetAllTextMaterials(id);
            ViewBag.Videos = await _videoService.GetAllVideos(id);

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
            if (ModelState.IsValid)
            {
                Module m = await _moduleService.AddModule(id, module);
                if(m != null)
                    return RedirectToAction("Details", "Courses", new { id = m.CourseId });
            }            
            return View(@module);
        }



        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await _moduleService.GetModule(id);
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
                Module new_module = await _moduleService.EditModule(id, module);

                if(new_module != null)
                    return RedirectToAction("Details", "Modules", new { id = id });                
            }            
            return View(@module);
        }

        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @module = await _moduleService.GetModule(id); 

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
            var module = await _moduleService.GetModule(id);
            await _moduleService.DeleteModuleById(id);
            return RedirectToAction("Details", "Courses", new { id = module.CourseId });
        }
    }
}
