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
    public class VideosController : Controller
    {
        private readonly CourseDbContext _context;

        public VideosController(CourseDbContext context)
        {
            _context = context;
        }

        // GET: Videos
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Videos.Include(v => v.Module);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Videos/Details/5
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

        // GET: Videos/Create
        [Authorize(Roles = "admin,teacher")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Videos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,teacher")]
        public async Task<IActionResult> Create(Guid id,[Bind("Id,Name,Url,Moduleid")] Video video)
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

        // GET: Videos/Edit/5
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
            ViewData["Moduleid"] = new SelectList(_context.Modules, "Id", "Id", video.Moduleid);
            return View(video);
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Url,Moduleid")] Video video)
        {
            if (id != video.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(video);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideoExists(video.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Modules", new { id = video.Moduleid });
            }
            ViewData["Moduleid"] = new SelectList(_context.Modules, "Id", "Id", video.Moduleid);
            return View(video);
        }

        // GET: Videos/Delete/5
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

        // POST: Videos/Delete/5
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
