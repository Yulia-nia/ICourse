﻿using System;
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
    public class PodcastsController : Controller
    {
        private readonly CourseDbContext _context;

        public PodcastsController(CourseDbContext context)
        {
            _context = context;
        }

        // GET: Podcasts
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Podcasts.Include(p => p.Module);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Podcasts/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podcast = await _context.Podcasts
                .Include(p => p.Module)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (podcast == null)
            {
                return NotFound();
            }

            return View(podcast);
        }

        // GET: Podcasts/Create
        [Authorize(Roles = "admin,teacher")]
        public IActionResult Create()
        {            
            return View();
        }

        // POST: Podcasts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,teacher")]
        public async Task<IActionResult> Create(Guid id, [Bind("Id,Name,Duration,Content,ModuleId")] Podcast podcast)
        {
            if (ModelState.IsValid)
            {
                podcast.Id = Guid.NewGuid();
                podcast.ModuleId = _context.Modules.FirstOrDefault(_ => _.Id == id).Id;

                _context.Add(podcast);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Modules", new { id = podcast.ModuleId });
            }
            
            return View(podcast);
        }

        // GET: Podcasts/Edit/5
        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podcast = await _context.Podcasts.FindAsync(id);
            if (podcast == null)
            {
                return NotFound();
            }
            ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Id", podcast.ModuleId);
            return View(podcast);
        }

        // POST: Podcasts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Duration,Content,ModuleId")] Podcast podcast)
        {
            if (id != podcast.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(podcast);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PodcastExists(podcast.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Details", "Modules", new { id = podcast.ModuleId });
            }
            ViewData["ModuleId"] = new SelectList(_context.Modules, "Id", "Id", podcast.ModuleId);
            return View(podcast);
        }

        // GET: Podcasts/Delete/5
        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var podcast = await _context.Podcasts
                .Include(p => p.Module)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (podcast == null)
            {
                return NotFound();
            }

            return View(podcast);
        }

        // POST: Podcasts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var podcast = await _context.Podcasts.FindAsync(id);
            _context.Podcasts.Remove(podcast);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Modules", new { id = podcast.ModuleId });
        }

        private bool PodcastExists(Guid id)
        {
            return _context.Podcasts.Any(e => e.Id == id);
        }
    }
}
