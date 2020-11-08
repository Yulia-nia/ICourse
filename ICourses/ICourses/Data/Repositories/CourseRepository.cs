using ICourses.Data.Interfaces;
using ICourses.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Repositories
{
    public class CourseRepository : ICourse
    {
        private readonly AppDbContext _appDbContext;
        public CourseRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public void AddCourse(Course course)
        {
            _appDbContext.Courses.Add(course);
            _appDbContext.SaveChanges();
        }

        public void DeleteCourse(Course course)
        {
            _appDbContext.Courses.Remove(course);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return _appDbContext.Courses.ToList();
        }

        public Course GetCourse(Guid id)
        {
            return _appDbContext.Courses.Where(x => x.Id == id).FirstOrDefault();
        }
        public void UpdateCourse(Course course)
        {
            _appDbContext.Courses.Update(course);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Comment> GetComments(Course course)
        {
            var comment = _appDbContext.Courses.Where(c => c.Id == course.Id)?.SelectMany(c => c.Comments).ToList();
            return comment.AsReadOnly();
        }

        public IEnumerable<Course> GetFavoriteCourses()
        {
            return _appDbContext.Courses.Where(x => x.IsFavorite == true).ToList();
        }

        //получить все избранные курсы
        public IEnumerable<Course> AllCourseIsFavorite()
        {
            return _appDbContext.Courses.Where(x => x.IsFavorite == true).ToList();
        }

        // получить все модули курса
        public IEnumerable<Module> AllThemsPost(Guid idCourse)
        {
            return _appDbContext.Modules.Where(x => x.CourseId == idCourse).ToList();
        }

    }
}
