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
        void DeleteCommentById(int id);
        Comment GetComment(int id);
        void UpdateComment(Comment comment);
    }
}
