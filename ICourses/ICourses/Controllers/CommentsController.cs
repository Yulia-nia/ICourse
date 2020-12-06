using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ICourses.Interfaces;
using ICourses.Services.Interfaces;
using ICourses.ViewModels;
using ICourses.Entities;

namespace ICourses.Views
{
    public class CommentsController : Controller
    {
        UserManager<User> _userManager;
        private readonly ICommentService _commentService;

        public CommentsController(UserManager<User> userManager, ICommentService commentService)
        {
            _commentService = commentService;
            _userManager = userManager;           
        }

        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _commentService.GetComment(id);

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
        public async Task<IActionResult> Create(Guid id,/* [Bind("Id,Title,Text,UserId,CourseId,Modified")] Comment comment*/ CommentViewModel comment)
        {
            string uid = _userManager.GetUserId(User);
            User user = await _userManager.FindByIdAsync(uid);


            if (ModelState.IsValid)
            {
                Comment com = await _commentService.AddComment(id, uid, comment);
                if(com != null)
                    return RedirectToAction("Details", "Courses", new { id = com.CourseId });
            }
            return View(comment);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _commentService.GetComment(id);
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
            var comment = await _commentService.GetComment(id);
            await _commentService.DeleteCommnet(id);
            return RedirectToAction("Details", "Courses", new { id = comment.CourseId });
        }
    }
}
