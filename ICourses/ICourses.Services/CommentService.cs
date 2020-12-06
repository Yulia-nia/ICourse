using ICourses.Interfaces;
using ICourses.Services.Interfaces;
using ICourses.Entities;
using ICourses.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Services
{

    public class CommentService : ICommentService
    {
        private readonly IComment _commentRepository;

        public CommentService(IComment comment)
        {
            _commentRepository = comment;
        }

        public async Task<Comment> AddComment(Guid id, string userId, CommentViewModel comment)
        {
            Comment com = new Comment
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Text = comment.Text,
                Title = comment.Title,
                CourseId = id,
                Modified = DateTime.Now,
            };

            await _commentRepository.AddComment(com);
            return com;
        }

        public async Task DeleteCommnet(Guid id)
        {
            await _commentRepository.DeleteCommentById(id);
        }

        public async Task<IEnumerable<Comment>> GetAllComments(Guid id)
        {
            return await _commentRepository.GetAllComments(id);
        }

        public async Task<Comment> GetComment(Guid id)
        {
            return await _commentRepository.GetComment(id);
        }
    }
}
