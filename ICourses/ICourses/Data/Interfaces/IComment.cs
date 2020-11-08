using ICourses.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Interfaces
{
    public interface IComment
    {
        void AddComment(Comment comment);
        IEnumerable<Comment> GetAllComments();
        void DeleteComment(Comment comment);
        void DeleteCommentById(Guid id);
        Comment GetComment(Guid id);
        void UpdateComment(Comment comment);
    }
}
