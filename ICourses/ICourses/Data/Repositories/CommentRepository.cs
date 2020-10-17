using ICourses.Data.Interfaces;
using ICourses.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Repositories
{
    public class CommentRepository : IComment
    {
        private readonly AppDbContext _appDbContext;
        public CommentRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        
        public void AddComment(Comment comment)
        {
            _appDbContext.Comments.Add(comment);
            _appDbContext.SaveChanges();
        }

        public void DeleteComment(Comment comment)
        {
            _appDbContext.Comments.Remove(comment);
            _appDbContext.SaveChanges();
        }
        public void DeleteCommentById(int id)
        {
            var comment = _appDbContext.Comments.Where(x => x.Id == id).FirstOrDefault();
            _appDbContext.Comments.Remove(comment);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Comment> GetAllComments()
        {
            return _appDbContext.Comments.ToList();
        }

        public Comment GetComment(int id)
        {
            return _appDbContext.Comments.Where(x => x.Id == id).FirstOrDefault();
        }

        public void UpdateComment(Comment comment)
        {
            _appDbContext.Comments.Update(comment);
            _appDbContext.SaveChanges();
        }

    }
}
