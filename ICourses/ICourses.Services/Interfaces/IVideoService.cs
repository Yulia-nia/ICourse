using ICourses.Entities;
using ICourses.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Services.Interfaces
{
    public interface IVideoService
    {
        Task<Video> AddVideo(Guid id, VideoViewModel video);
        Task<Video> EditVideo(Guid id, VideoViewModel video);
        Task<IEnumerable<Video>> GetAllVideos(Guid id);
        Task DeleteVideoById(Guid id);
        Task<Video> GetVideo(Guid id);
    }
}
