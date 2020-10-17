using ICourses.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Interfaces
{
    public interface ILike
    {
        void AddLike(Like like);
        IEnumerable<Like> GetAllLikes();
        void DeleteLike(Like like);
        void DeleteLikeById(int id);
        Like GetLike(int id);
        void UpdateLike(Like like);
    }
}
