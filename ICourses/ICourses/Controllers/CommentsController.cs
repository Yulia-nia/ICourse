using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ICourses.Data;
using ICourses.Data.Models;
using Microsoft.AspNetCore.Identity;
using ICourses.Data.Interfaces;

namespace ICourses.Views
{
    public class CommentsController : Controller
    {
        UserManager<User> _userManager;
        private readonly CourseDbContext _context;
        private readonly IComment _comments;

        public CommentsController(CourseDbContext context, UserManager<User> userManager, IComment comments)
        {
            _comments = comments;
            _userManager = userManager;
            _context = context;
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Course)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        public IActionResult Create()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Guid id, [Bind("Id,Title,Text,UserId,CourseId,Modified")] Comment comment)
        {
            string uid = _userManager.GetUserId(User);
            User user = await _userManager.FindByIdAsync(uid);

            if (ModelState.IsValid)
            {
                comment.Id = Guid.NewGuid();
                comment.Modified = DateTime.Now;
                comment.CourseId = _context.Courses.FirstOrDefault(_ => _.Id == id).Id;
                comment.UserId = user.Id;

                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Courses", new { id = comment.CourseId });
            }
            return View(comment);
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments
                .Include(c => c.Course)
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var comment = await _context.Comments.FindAsync(id);
            await _comments.DeleteCommentById(comment.Id);

           
            return RedirectToAction("Details", "Courses", new { id = comment.CourseId });
        }

        private bool CommentExists(Guid id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
