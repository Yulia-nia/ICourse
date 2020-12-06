using ICourses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Interfaces
{
    public interface ILike
    {
        Task AddLike(Like like);
        Task<IEnumerable<Like>> GetAllLikes(string id);
        Task DeleteLikeById(Guid id);
        Task<Like> GetLike(Guid id);
        //Task UpdateLike(Like like);
    }
}
