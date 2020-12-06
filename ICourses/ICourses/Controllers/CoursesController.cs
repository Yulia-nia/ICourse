using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ICourses.ViewModels;
using ICourses.Interfaces;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using ICourses.Services.Interfaces;
using ICourses.Entities;

namespace ICourses.Controllers
{
    [Authorize]
    public class CoursesController : Controller
    {
        UserManager<User> _userManager;
        //private readonly CourseDbContext _context;
        //private readonly ICourse _course;
        private readonly ICommentService _commentService;

        private readonly ICourseService _courseService;
        private readonly ILikeService _like;
        public CoursesController(ICourseService courseService, UserManager<User> userManager, ILikeService like, ICommentService commentService)
        {
            _commentService = commentService;
            _like = like;
            _courseService = courseService;
            _userManager = userManager;     
        }


        public async Task<IActionResult> AddFavorite(Guid id)
        {
            var getCourse = await _courseService.GetCourse(id); //
            var getUser = _userManager.GetUserId(User);         //

            // добавляем в список юзера
            if (getCourse != null)
            {
                var like = _like.AddLike(id, getUser);
                if (like != null)
                {
                    return RedirectToAction("Details", "Courses", new { id = id });
                }                
            }
            return RedirectToAction("Details", "Courses", new { id = id });
        }

        public async Task<IActionResult> GetFavorite()
        {
            //var getCourse = await _courseService.GetCourse(id); //
            var getUser = _userManager.GetUserId(User);         //
            var likes = await _like.GetAllLikes(getUser);
            return View(likes);
        }


        [Authorize(Roles = "admin,teacher,moderator")]
        public async Task<IActionResult> UserCourses()
        {
            //var getCourse = await _courseService.GetCourse(id); //
            var getUser = _userManager.GetUserId(User);         //
            var course = await _courseService.GetUserCourses(getUser);
            return View(course);
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseService.GetCourse(id);
            ViewBag.Comments = await _commentService.GetAllComments(id);

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
            string uid = _userManager.GetUserId(User);
            User user = await _userManager.FindByIdAsync(uid);

            if (ModelState.IsValid)
            {
                var result = await _courseService.AddCourse(id, course, uid);
                if (result != null)
                {
                    return RedirectToAction("Details", "Subjects", new { id = result.SubjectId });
                }                    
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

            Course course = await _courseService.GetCourse(id);

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
                Course new_course = await _courseService.EditCourse(id, course);

                if (new_course != null)
                {                   
                    return RedirectToAction("Details", "Subjects", new { id = new_course.SubjectId });
                }    
            }           
            return View(course);
        }
        
        
        [Authorize(Roles = "admin,moderator,teacher")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _courseService.GetCourse(id);
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
            var course = await _courseService.GetCourse(id);
            await _courseService.DeleteCourse(course);
            return RedirectToAction("Details", "Subjects", new { id = course.SubjectId });
        }


        //public async Task<IActionResult> Like(Guid id)
        //{
        //    Course update = _context.Courses.ToList().Find(u => u.Id == id);
            
        //    update.LikePost += 1;
        //    _appDbContext.SaveChanges();

        //    return RedirectToAction("Details", "Courses", new { id = id });
          
        //}

    }
}
