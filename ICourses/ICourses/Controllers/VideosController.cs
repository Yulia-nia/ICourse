using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ICourses.ViewModels;
using ICourses.Entities;
using Microsoft.AspNetCore.Authorization;
using ICourses.Interfaces;
using ICourses.Services.Interfaces;

namespace ICourses.Controllers
{
    [Authorize]
    public class VideosController : Controller
    {
        //private readonly CourseDbContext _context;
        //private readonly IVideo _video;
        IVideoService _videService;
       public VideosController(/*CourseDbContext context*/ IVideoService videoService)
        {
            //_video = video;
            _videService = videoService;
           // _context = context;
        }

        //public async Task<IActionResult> Index()
        //{
        //    //var appDbContext = _context.Videos.Include(v => v.Module);
        //    return View(await _videService.GetAllVideos());
        //}

        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await _videService.GetVideo(id);

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
        public async Task<IActionResult> Create(Guid id, VideoViewModel video)//[Bind("Id,Name,Url")] Video video)
        {
            if (ModelState.IsValid)
            {
                Video new_video = await _videService.AddVideo(id, video);

                if(new_video != null)
                    return RedirectToAction("Details", "Modules", new { id = new_video.Moduleid });
            }
           
            return View(video);
        }

        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await _videService.GetVideo(id);
            if (video == null)
            {
                return NotFound();
            }
            return View(video);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> Edit(Guid id, VideoViewModel video)
        {
            if (ModelState.IsValid)
            {
                Video v = await _videService.EditVideo(id, video);
                if(v != null)
                    return RedirectToAction("Details", "Modules", new { id = v.Moduleid });                
            }            
            return View(video);
        }

        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var video = await _videService.GetVideo(id);
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
            var video = await _videService.GetVideo(id);              
            await _videService.DeleteVideoById(id);           
            return RedirectToAction("Details", "Modules", new { id = video.Moduleid });
        }

       
    }
}
