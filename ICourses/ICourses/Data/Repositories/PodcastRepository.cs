using ICourses.Data.Interfaces;
using ICourses.Data. Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Repositories
{
    public class PodcastRepository : IPodcast
    {
        private readonly CourseDbContext _appDbContext;

        public PodcastRepository(CourseDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public async Task AddPodcast(Podcast podcast)
        {
            await _appDbContext.Podcasts.AddAsync(podcast);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeletePodcast(Podcast podcast)
        {
            _appDbContext.Podcasts.Remove(podcast);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeletePodcastById(Guid id)
        {
            var podcast = await _appDbContext.Podcasts.Where(x => x.Id == id).FirstOrDefaultAsync();
            _appDbContext.Podcasts.Remove(podcast);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Podcast>> GetAllPodcasts()
        {
            return await _appDbContext.Podcasts.ToListAsync();
        }

        public async Task<Podcast> GetPodcast(Guid id)
        {
            return await _appDbContext.Podcasts.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdatPodcaste(Podcast podcast)
        {
            _appDbContext.Podcasts.Update(podcast);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
