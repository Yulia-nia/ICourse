using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ICourses.Interfaces;
using ICourses.Services.Interfaces;
using ICourses.Entities;
using ICourses.ViewModels;

namespace ICourses.Services
{
    public class LikeService : ILikeService
    {
        private readonly ILike _likeRepository;
        private readonly ICourseService _courseService;
        public LikeService(ILike likeService, ICourseService courseService)
        {
            _courseService = courseService;
            _likeRepository = likeService;
        }

        public async Task<Like> AddLike(Guid courseId, string userId)
        {
            Like like = new Like
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                CourseId = courseId,
            };
            await _likeRepository.AddLike(like);
            return like;
        }

        public async Task DeleteLikeById(Guid id)
        {
            await _likeRepository.DeleteLikeById(id);
        }

        public async Task<IEnumerable<Course>> GetAllLikes(string id)
        {
            var courses = new List<Course>();
            var likes = await _likeRepository.GetAllLikes(id);

            foreach(var l in likes)
            {
                courses.Add(await _courseService.GetCourse(l.CourseId));
            }
            return courses;
        }

        public async Task<Like> GetLike(Guid id)
        {
            return await _likeRepository.GetLike(id);
        }
    }
}
