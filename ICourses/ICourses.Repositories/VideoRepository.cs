using ICourses.Interfaces;
using ICourses.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Repositories
{
    public class VideoRepository : IVideo
    {
        private readonly CourseDbContext _appDbContext;

        public VideoRepository(CourseDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public async Task AddVideo(Video video)
        {
            await _appDbContext.Videos.AddAsync(video);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteVideo(Video video)
        {
            _appDbContext.Videos.Remove(video);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteVideoById(Guid id)
        {
            var video = await _appDbContext.Videos.Where(x => x.Id == id).FirstOrDefaultAsync();
            _appDbContext.Videos.Remove(video);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Video>> GetAllVideos(Guid id)
        {
            var module = await _appDbContext.Modules.Where(_ => _.Id == id).FirstOrDefaultAsync();
            return module.Videos.ToList();
        }

        public async Task<Video> GetVideo(Guid id)
        {
            return await _appDbContext.Videos.Where(x => x.Id == id).Include(v => v.Module).FirstOrDefaultAsync();
        }

        public async Task UpdateVideo(Video video)
        {
            _appDbContext.Videos.Update(video);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
