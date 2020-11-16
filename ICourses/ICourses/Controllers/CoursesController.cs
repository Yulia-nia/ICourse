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

        private readonly ILike _like;
        public CoursesController(CourseDbContext context, UserManager<User> userManager, ICourse course, ILike like)
        {
            _like = like;
            _course = course;
            _userManager = userManager;
            _context = context;
        }

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
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course course = await _course.GetCourse(id);

            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> Edit(Guid id, EditCourseViewModel course)
        {
            if (ModelState.IsValid)
            {
                byte[] imageData = null;

                using (var binaryReader = new BinaryReader(course.Image.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)course.Image.Length);
                }

                Course new_course = await _course.GetCourse(id);

                if (new_course != null)
                {
                    new_course.Name = course.Name;
                    new_course.Description = course.Description;
                    new_course.Image = imageData;

                    await _course.UpdateCourse(new_course);
                    return RedirectToAction("Details", "Subjects", new { id = new_course.SubjectId });
                }    
            }           
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

        //public async Task<IActionResult> Like(Guid id)
        //{
        //    Course update = _context.Courses.ToList().Find(u => u.Id == id);
            
        //    update.LikePost += 1;
        //    _appDbContext.SaveChanges();

        //    return RedirectToAction("Details", "Courses", new { id = id });
          
        //}

        [HttpPost]
        [Route("addLike")]
        public async Task<IActionResult> LikeCourse([FromForm] Guid courseId)
        {
            if (ModelState.IsValid && courseId != null)
            {
                string uid = _userManager.GetUserId(User);
                User user = await _userManager.FindByIdAsync(uid);

                if (uid != null)
                {                   
                    Like newLike = new Like()
                    {
                        Id = Guid.NewGuid(),
                        CourseId = courseId,
                        UserId = uid,
                    };
                    await _like.AddLike(newLike);

                    if (newLike != null)
                        return Ok(newLike);
                }
            }
            return Unauthorized();
        }


        [HttpDelete]
        [Route("removeLike")]
        public async Task<IActionResult> RemoveLike([FromForm] Guid courseId)
        {
            if (ModelState.IsValid && courseId != null)
            {
                string uid = _userManager.GetUserId(User);
                User user = await _userManager.FindByIdAsync(uid);

                if (uid != null)
                {
                    await _course.RemoveLike(courseId, uid);
                    return Ok();
                }
            }
            return Unauthorized();
        }
    }
}
