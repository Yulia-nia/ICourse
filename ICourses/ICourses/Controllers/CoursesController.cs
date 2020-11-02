using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ICourses.Data;
using ICourses.Data.Models;
using ICourses.ViewModel;
using ICourses.Data.Interfaces;
using System.IO;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace ICourses.Controllers
{
    public class CoursesController : Controller
    {
        private readonly AppDbContext _context;

        // private readonly IUser _user;
        private readonly ICourse _course;
        private readonly ISubject _subject;

        public CoursesController(AppDbContext context, ICourse course, ISubject subject)
        {
            _course = course;
            _subject = subject;
            _context = context;
        }

        // GET: Courses
        [Authorize]
        public async Task<IActionResult> Index(int id)
        {
            var appDbContext = _context.Courses.Where(c => c.SubjectID == id);
            ViewBag.Description = _context.Subjects.FirstOrDefault(c => c.Id == id).Description;
            ViewBag.NameCourse = _context.Subjects.FirstOrDefault(c => c.Id == id).Name;
            return View(await appDbContext.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            ViewData["SubjectID"] = new SelectList(_context.Subjects, "Id", "Name");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Modified,Language,IsFavorite,SubjectID")] Course course)
        {

            //byte[] imageData = null;
            //// считываем переданный файл в массив байтов
            //using (var binaryReader = new BinaryReader(course.Image.Path.OpenReadStream()))
            //{
            //    imageData = binaryReader.ReadBytes((int)course.Image.Path.Length);
            //}


            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectID"] = new SelectList(_context.Subjects, "Id", "Id", course.SubjectID);
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["SubjectID"] = new SelectList(_context.Subjects, "Id", "Id", course.SubjectID);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Modified,Language,IsFavorite,SubjectID")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectID"] = new SelectList(_context.Subjects, "Id", "Id", course.SubjectID);
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Courses
                .Include(c => c.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }



        public async Task<ActionResult> AttachImage(int id)
        {
            Course p = _course.GetCourse(id);
            if (p == null)
                return NotFound();
            return View(p);
        }

        /*
        [HttpPost]
        public async Task<ActionResult> AttachImage(string id, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                _course.StoreImage(id, uploadedFile.OpenReadStream(), uploadedFile.FileName);
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> GetImage(int id)
        {
            var image = _course.GetCourse(id);
            if (image == null)
            {
                return NotFound();
            }
            return File(image, "image/png");
        }
        */
    }
}
