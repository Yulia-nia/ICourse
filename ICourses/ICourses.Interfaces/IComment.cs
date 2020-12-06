using ICourses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Interfaces
{
    public interface IComment
    {
        Task AddComment(Comment comment);
        Task<IEnumerable<Comment>> GetAllComments(Guid id);
        Task DeleteComment(Comment comment);
        Task DeleteCommentById(Guid id);
        Task<Comment> GetComment(Guid id);
        Task UpdateComment(Comment comment);
    }
}
