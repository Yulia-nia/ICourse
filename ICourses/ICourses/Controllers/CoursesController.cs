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
    [Authorize]
    public class CoursesController : Controller
    {
        private readonly AppDbContext _context;

        // private readonly IUser _user;
 
        //private readonly ISubject _subject;

        public CoursesController(AppDbContext context/*, ISubject subject*/)
        {
            
            //_subject = subject;
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index(int id)
        {
            //подключить другие моделькиии
            var appDbContext = _context.Courses.Where(c => c.SubjectID == id).Include(c => c.Author);

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
                .Include(c => c.Author)
                .Include(c => c.Modules)
                .FirstOrDefaultAsync(m => m.Id == id);



            ViewBag.Comments = _context.Comments.Include(c => c.Course).Include(c => c.User).Where(_ => _.CourseId == id);

            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        [Authorize(Roles = "admin,teacher")]
        public IActionResult Create(int? id)
        {
            //ViewData["SubjectID"] = _context.Subjects.FirstOrDefault(_ => _.Id == id).Id;



            ViewData["SubjectID"] = new SelectList(_context.Subjects, "Id", "Name");
            ViewData["AuthorID"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,teacher")]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Modified,Language,IsFavorite,SubjectID,AuthorID")] Course course)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Subjects", new { id = course.SubjectID });
            }
            //ViewData["SubjectID"] = _context.Subjects.FirstOrDefault(_ => _.Id == SubjectID).Id;
            ViewData["SubjectID"] = new SelectList(_context.Subjects, "Id", "Id", course.SubjectID);
            ViewData["AuthorID"] = new SelectList(_context.Users, "Id", "Id", course.AuthorID);
            return View(course);
        }

        // GET: Courses/Edit/5
        [Authorize(Roles = "admin,moderator,teacher")]
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
        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Modified,Language,IsFavorite,SubjectID,AuthorID")] Course course)
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
                return RedirectToAction("Details", "Subjects", new { id = course.SubjectID });
            }
            ViewData["SubjectID"] = new SelectList(_context.Subjects, "Id", "Id", course.SubjectID);
            ViewData["AuthorID"] = new SelectList(_context.Users, "Id", "Id", course.AuthorID);
            return View(course);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles = "admin,moderator,teacher")]
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
        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Subjects", new { id = course.SubjectID });
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
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
