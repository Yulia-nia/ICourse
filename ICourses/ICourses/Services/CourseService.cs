using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ICourses.Data.Interfaces;
using ICourses.Data.Models;
using ICourses.Services.Interfaces;
using ICourses.ViewModel;

namespace ICourses.Services
{
    public class CourseService : ICourseService
    {
        ICourse _coursesRepository;
        ISubjectService _subjectService;
        public CourseService(ICourse coursesRepository, ISubjectService subjectService)
        {
            _subjectService = subjectService;
            _coursesRepository = coursesRepository;
        }

        public async Task<Course> AddCourse(Guid id, CreateCourseViewModel course, string userId)
        {
            byte[] imageData = null;

            using (var binaryReader = new BinaryReader(course.Image.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)course.Image.Length);
            }

            Subject subject = await _subjectService.GetSubject(id);

            Course new_course = new Course()
            {
                Id = Guid.NewGuid(),
                Modified = DateTime.Now,
                Name = course.Name,
                Description = course.Description,
                Language = course.Language,
                Image = imageData,
                SubjectId = subject.Id,
                AuthorId = userId,
            };

            await _coursesRepository.AddCourse(new_course);
            return new_course;
        }

        public async Task DeleteCourse(Course course)
        {
            await _coursesRepository.DeleteCourse(course);
        }


        public async Task<Course> GetCourse(Guid id)
        {
            var course = await _coursesRepository.GetCourse(id);
            return course;
        }    

        public async Task UpdateCourse(Course course)
        {
            await _coursesRepository.UpdateCourse(course);
        }


        //public Task<IEnumerable<Course>> GetallCourses()
        //{
        //    throw new notimplementedexception();
        //}


        //-
        public async Task<IEnumerable<Course>> GetFavoriteCourses()
        {
            return await _coursesRepository.GetFavoriteCourses();
        }



        public async Task<IEnumerable<Comment>> GetComments(Course course)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Like>> GetLikes(Guid postId)
        {
            throw new NotImplementedException();
        }

        public async Task RemoveLike(Guid postId, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<Course> EditCourse(Guid id, EditCourseViewModel course)
        {
            byte[] imageData = null;

            using (var binaryReader = new BinaryReader(course.Image.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)course.Image.Length);
            }

            Course new_course = await _coursesRepository.GetCourse(id);

            if (new_course != null)
            {
                new_course.Name = course.Name;
                new_course.Description = course.Description;
                new_course.Image = imageData;

                await _coursesRepository.UpdateCourse(new_course);
            }
            return new_course;
        }
    }
}
