using ICourses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Services.Interfaces
{
    public interface ILikeService
    {
        Task<Like> AddLike(Guid courseId, string userId);
        Task DeleteLikeById(Guid id);
        Task<IEnumerable<Course>> GetAllLikes(string id);
        Task<Like> GetLike(Guid id);
    }
}
