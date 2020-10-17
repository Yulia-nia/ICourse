using ICourses.Data.Interfaces;
using ICourses.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Repositories
{
    public class LikeRepository : ILike
    {
        private readonly AppDbContext _appDbContext;

        public LikeRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
       
        public void AddLike(Like like)
        {
            _appDbContext.Likes.Add(like);
            _appDbContext.SaveChanges();
        }

        public void DeleteLike(Like like)
        {
            _appDbContext.Likes.Remove(like);
            _appDbContext.SaveChanges();
        }

        public void DeleteLikeById(int id)
        {
            var like = _appDbContext.Likes.Where(x => x.Id == id).FirstOrDefault();
            _appDbContext.Likes.Remove(like);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Like> GetAllLikes()
        {
            return _appDbContext.Likes.ToList();
        }

        public Like GetLike(int id)
        {
            return _appDbContext.Likes.Where(x => x.Id == id).FirstOrDefault();
        }

        public void UpdateLike(Like like)
        {
            _appDbContext.Likes.Update(like);
            _appDbContext.SaveChanges();
        }
    }
}
