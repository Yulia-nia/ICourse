using ICourses.Entities;
using ICourses.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Services.Interfaces
{
    public interface ICourseService
    {
        Task<Course> AddCourse(Guid id, CreateCourseViewModel course, string userId);
        Task<Course> EditCourse(Guid id, EditCourseViewModel course);

        //Task<IEnumerable<Course>> GetAllCourses();
        Task DeleteCourse(Course course);
        Task<Course> GetCourse(Guid id);
        Task UpdateCourse(Course course);
        Task RemoveLike(Guid postId, string userId);
        Task<IEnumerable<Like>> GetLikes(Guid postId);
        Task<IEnumerable<Comment>> GetComments(Course course);

        Task<IEnumerable<Course>> GetUserCourses(string id);
        Task<IEnumerable<Course>> GetFavoriteCourses(Guid id);
    }
}
