using ICourses.Data.Interfaces;
using ICourses.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Repositories
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

        public async Task<IEnumerable<Video>> GetAllVideos()
        {
            return await _appDbContext.Videos.ToListAsync();
        }

        public async Task<Video> GetVideo(Guid id)
        {
            return await _appDbContext.Videos.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateVideo(Video video)
        {
            _appDbContext.Videos.Update(video);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
