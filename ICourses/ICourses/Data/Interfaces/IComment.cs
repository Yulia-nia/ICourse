using ICourses.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Interfaces
{
    public interface IComment
    {
        Task AddComment(Comment comment);
        Task<IEnumerable<Comment>> GetAllComments();
        Task DeleteComment(Comment comment);
        Task DeleteCommentById(Guid id);
        Task<Comment> GetComment(Guid id);
        Task UpdateComment(Comment comment);
    }
}
