using ICourses.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Interfaces
{
    public interface ICourse
    {
        Task AddCourse(Course course);
        Task<IEnumerable<Course>> GetAllCourses();
        Task DeleteCourse(Course course);
        Task<Course> GetCourse(Guid id);
        Task UpdateCourse(Course course);

        Task<IEnumerable<Comment>> GetComments(Course course);
        Task<IEnumerable<Course>> GetFavoriteCourses();
    }
}
