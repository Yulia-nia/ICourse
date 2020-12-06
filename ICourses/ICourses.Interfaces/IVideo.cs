using ICourses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Interfaces
{
    public interface IVideo
    {
        Task AddVideo(Video video);
        Task<IEnumerable<Video>> GetAllVideos(Guid id);
        Task DeleteVideo(Video video);
        Task DeleteVideoById(Guid id);
        Task<Video> GetVideo(Guid id);
        Task UpdateVideo(Video video);

    }
}
