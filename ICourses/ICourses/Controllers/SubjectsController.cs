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
using ICourses.Services.Interfaces;

namespace ICourses.Controllers
{
    public class SubjectsController : Controller
    {       
        ISubjectService _subjectService;

        public SubjectsController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }
       
        public async Task<IActionResult> Index()
        {
            return View(await _subjectService.GetAllSubject());
        }
       
        public async Task<IActionResult> Details(Guid id)
        {
            var subject = await _subjectService.GetSubject(id);

            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }



        [Authorize(Roles = "admin,moderator")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,moderator")]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Subject subject)
        {
            if (ModelState.IsValid)
            { 
                await _subjectService.AddSubject(subject);
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        [Authorize(Roles = "admin,moderator")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var subject = await _subjectService.GetSubject(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,moderator")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description")] Subject subject)
        {
            if (id != subject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                await _subjectService.UpdateSubject(subject);     
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        [Authorize(Roles = "admin,moderator")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var subject = await _subjectService.GetSubject(id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,moderator")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var subject = await _subjectService.GetSubject(id);
            await _subjectService.DeleteSubjectById(id);
            return RedirectToAction(nameof(Index));
        }

        //private bool SubjectExists(Guid id)
        //{
        //    return _context.Subjects.Any(e => e.Id == id);
        //}
    }
}
