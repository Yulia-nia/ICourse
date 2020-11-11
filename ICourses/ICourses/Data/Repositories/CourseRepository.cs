using ICourses.Data.Interfaces;
using ICourses.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace ICourses.Data.Repositories
{
    public class CourseRepository : ICourse
    {
        private readonly CourseDbContext _appDbContext;
        public CourseRepository(CourseDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public async Task AddCourse(Course course)
        {
            await _appDbContext.Courses.AddAsync(course);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteCourse(Course course)
        {
            _appDbContext.Courses.Remove(course);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return await _appDbContext.Courses.ToListAsync();
        }

        public async Task<Course> GetCourse(Guid id)
        {
            return await _appDbContext.Courses.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task UpdateCourse(Course course)
        {
            _appDbContext.Courses.Update(course);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetComments(Course course)
        {
            var comment = await _appDbContext.Courses.Where(c => c.Id == course.Id)?.SelectMany(c => c.Comments).ToListAsync();
            return comment.AsReadOnly();
        }

        public async Task<IEnumerable<Course>> GetFavoriteCourses()
        {
            return await _appDbContext.Courses.Where(x => x.IsFavorite == true).ToListAsync();
        }

        public async Task<IEnumerable<Like>> GetLikes(Guid courseId)
        {          
            return await _appDbContext.Likes.Where(like => like.CourseId == courseId).ToListAsync();
        }

        public async Task RemoveLike(Guid courseId, string userId)
        {           
            var like = await _appDbContext.Likes.FirstOrDefaultAsync(li => li.CourseId == courseId && li.UserId == userId);
            _appDbContext.Likes.Remove(like);
            await _appDbContext.SaveChangesAsync();
        }

    }
}
