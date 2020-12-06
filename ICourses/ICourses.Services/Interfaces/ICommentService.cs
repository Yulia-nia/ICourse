using ICourses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ICourses.ViewModels;

namespace ICourses.Services.Interfaces
{
    public interface ICommentService
    {
        Task<Comment> GetComment(Guid id);
        Task<Comment> AddComment(Guid id, string userId, CommentViewModel comment);
        Task DeleteCommnet(Guid id);
        Task<IEnumerable<Comment>> GetAllComments(Guid id);

    }
}
