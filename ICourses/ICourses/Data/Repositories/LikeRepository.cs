using ICourses.Data.Interfaces;
using ICourses.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Repositories
{
    public class LikeRepository : ILike
    {
        private readonly CourseDbContext _appDbContext;

        public LikeRepository(CourseDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
       
        public async Task AddLike(Like like)
        {
            await _appDbContext.Likes.AddAsync(like);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteLike(Like like)
        {
            _appDbContext.Likes.Remove(like);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteLikeById(Guid id)
        {
            var like = await _appDbContext.Likes.Where(x => x.Id == id).FirstOrDefaultAsync();
            _appDbContext.Likes.Remove(like);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Like>> GetAllLikes()
        {
            return await _appDbContext.Likes.ToListAsync();
        }

        public async Task<Like> GetLike(Guid id)
        {
            return await _appDbContext.Likes.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        //public async Task UpdateLike(Like like)
        //{
        //    _appDbContext.Likes.Update(like);
        //    await _appDbContext.SaveChangesAsync();
        //}
    }
}
