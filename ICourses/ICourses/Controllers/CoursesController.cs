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
        UserManager<User> _userManager;
        private readonly CourseDbContext _context;

        private readonly ICourse _course;


        public CoursesController(CourseDbContext context, UserManager<User> userManager, ICourse course)
        {
            _course = course;
            _userManager = userManager;
            _context = context;
        }

        //++
        public async Task<IActionResult> Index(Guid id)
        {
            var appDbContext = _context.Courses.Where(c => c.SubjectId == id).Include(c => c.Author);
            ViewBag.Description = _context.Subjects.FirstOrDefault(c => c.Id == id).Description;
            ViewBag.NameCourse = _context.Subjects.FirstOrDefault(c => c.Id == id).Name;
            return View(await appDbContext.ToListAsync());
        }

        //++
        public async Task<IActionResult> Details(Guid? id)
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


        [Authorize(Roles = "admin,teacher")]
        public IActionResult Create(Guid? id)
        {            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,teacher")]
        public async Task<IActionResult> Create(Guid id, CreateCourseViewModel course)
        {

            byte[] imageData = null;

            using (var binaryReader = new BinaryReader(course.Image.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)course.Image.Length);
            }

            string uid = _userManager.GetUserId(User);
            User user = await _userManager.FindByIdAsync(uid);

            if (ModelState.IsValid)
            {
                Course new_course = new Course()
                {
                    Id = Guid.NewGuid(),
                    Modified = DateTime.Now,
                    Name = course.Name,
                    Description = course.Description,
                    Language = course.Language,
                    Image = imageData,
                    SubjectId = _context.Subjects.FirstOrDefault(_ => _.Id == id).Id,
                    AuthorId = user.Id,             
                };
                await _course.AddCourse(new_course);
                return RedirectToAction("Details", "Subjects", new { id = new_course.SubjectId });
            }
            return View();
        }


        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> Edit(Guid? id)
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
            ViewData["SubjectID"] = new SelectList(_context.Subjects, "Id", "Id", course.SubjectId);
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Description,Modified,Language,IsFavorite,SubjectID,AuthorID")] Course course)
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
                return RedirectToAction("Details", "Subjects", new { id = course.SubjectId });
            }
            ViewData["SubjectID"] = new SelectList(_context.Subjects, "Id", "Id", course.SubjectId);
            ViewData["AuthorID"] = new SelectList(_context.Users, "Id", "Id", course.SubjectId);
            return View(course);
        }
        
        
        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> Delete(Guid? id)
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

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var course = await _context.Courses.FindAsync(id);
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Subjects", new { id = course.SubjectId });
        }

        private bool CourseExists(Guid id)
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
