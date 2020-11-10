using ICourses.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Interfaces
{
    public interface ILike
    {
        Task AddLike(Like like);
        Task<IEnumerable<Like>> GetAllLikes();
        Task DeleteLike(Like like);
        Task DeleteLikeById(Guid id);
        Task<Like> GetLike(Guid id);
        Task UpdateLike(Like like);
    }
}
