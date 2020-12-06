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
    public class VideoService : IVideoService
    {
        private readonly IVideo _video;
        private readonly IModuleService _moduleService;

        public  VideoService(IVideo video, IModuleService moduleService)
        {
            _moduleService = moduleService;
            _video = video;
        }

        public async Task<Video> AddVideo(Guid id, VideoViewModel video)
        {
            var module = await _moduleService.GetModule(id);

            Video new_video = new Video
            {
                Id = Guid.NewGuid(),
                Name = video.Name,
                Url = video.Url,
                Moduleid = module.Id,
        };
            await _video.AddVideo(new_video); 
            return new_video;
        }

        public async Task DeleteVideoById(Guid id)
        {
            await _video.DeleteVideoById(id);
        }

        public async Task<Video> EditVideo(Guid id, VideoViewModel video)
        {
            Video new_video = await _video.GetVideo(id);

            if (new_video != null)
            {
                new_video.Name = video.Name;
                new_video.Url = video.Url;
                await _video.UpdateVideo(new_video);
            }
            return new_video;
        }

        public async Task<IEnumerable<Video>> GetAllVideos(Guid id)
        {
            return await _video.GetAllVideos(id);
        }

        public async Task<Video> GetVideo(Guid id)
        {
            return await _video.GetVideo(id);
        }
    }
}
