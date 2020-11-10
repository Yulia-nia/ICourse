using ICourses.Data.Interfaces;
using ICourses.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Repositories
{
    public class CommentRepository : IComment
    {
        private readonly CourseDbContext _appDbContext;
        public CommentRepository(CourseDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        
        public async Task AddComment(Comment comment)
        {
           await _appDbContext.Comments.AddAsync(comment);
           await  _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteComment(Comment comment)
        {
            _appDbContext.Comments.Remove(comment);
            await _appDbContext.SaveChangesAsync();
        }
        public async Task DeleteCommentById(Guid id)
        {
            var comment = _appDbContext.Comments.Where(x => x.Id == id).FirstOrDefault();
            _appDbContext.Comments.Remove(comment);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Comment>> GetAllComments()
        {
            return await _appDbContext.Comments.ToListAsync();
        }

        public async Task<Comment> GetComment(Guid id)
        {
            return await _appDbContext.Comments.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateComment(Comment comment)
        {
            _appDbContext.Comments.Update(comment);
            await _appDbContext.SaveChangesAsync();
        }

    }
}
