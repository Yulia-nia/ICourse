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
using ICourses.ViewModel;
using ICourses.Data.Interfaces;

namespace ICourses.Controllers
{
    [Authorize]
    public class VideosController : Controller
    {
        private readonly CourseDbContext _context;
        private readonly IVideo _video;
       public VideosController(CourseDbContext context, IVideo video)
        {
            _video = video;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Videos.Include(v => v.Module);
            return View(await appDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await _context.Videos
                .Include(v => v.Module)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        [Authorize(Roles = "admin,teacher")]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,teacher")]
        public async Task<IActionResult> Create(Guid id,[Bind("Id,Name,Url")] Video video)
        {
            if (ModelState.IsValid)
            {
                video.Id = Guid.NewGuid();
                video.Moduleid = _context.Modules.FirstOrDefault(_ => _.Id == id).Id;

                _context.Add(video);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Modules", new { id = video.Moduleid });
            }
           
            return View(video);
        }

        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await _context.Videos.FindAsync(id);
            if (video == null)
            {
                return NotFound();
            }
            //ViewData["Moduleid"] = new SelectList(_context.Modules, "Id", "Id", video.Moduleid);
            return View(video);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> Edit(Guid id, ChangeVideoViewModel video)
        {
            if (ModelState.IsValid)
            {
                Video new_video = await _video.GetVideo(id);
                new_video.Name = video.Name;
                new_video.Url = video.Url;
                await _video.UpdateVideo(new_video);
                return RedirectToAction("Details", "Modules", new { id = new_video.Moduleid });                
            }            
            return View(video);
        }

        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await _context.Videos
                .Include(v => v.Module)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (video == null)
            {
                return NotFound();
            }

            return View(video);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var video = await _context.Videos.FindAsync(id);
            _context.Videos.Remove(video);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Modules", new { id = video.Moduleid });
        }

        private bool VideoExists(Guid id)
        {
            return _context.Videos.Any(e => e.Id == id);
        }
    }
}
