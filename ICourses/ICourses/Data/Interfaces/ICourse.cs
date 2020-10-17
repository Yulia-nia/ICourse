using ICourses.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Interfaces
{
    public interface ICourse
    {
        void AddCourse(Course course);
        IEnumerable<Course> GetAllCourses();
        void DeleteCourse(Course course);
        Course GetCourse(int id);
        void UpdateCourse(Course course);

        IEnumerable<Comment> GetComments(Course course);
        IEnumerable<Course> GetFavoriteCourses();
    }
}
