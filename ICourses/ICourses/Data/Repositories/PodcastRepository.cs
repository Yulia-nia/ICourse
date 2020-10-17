using ICourses.Data.Interfaces;
using ICourses.Data. Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Repositories
{
    public class PodcastRepository : IPodcast
    {
        private readonly AppDbContext _appDbContext;

        public PodcastRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public void AddPodcast(Podcast podcast)
        {
            _appDbContext.Podcasts.Add(podcast);
            _appDbContext.SaveChanges();
        }

        public void DeletePodcast(Podcast podcast)
        {
            _appDbContext.Podcasts.Remove(podcast);
            _appDbContext.SaveChanges();
        }

        public void DeletePodcastById(int id)
        {
            var podcast = _appDbContext.Podcasts.Where(x => x.Id == id).FirstOrDefault();
            _appDbContext.Podcasts.Remove(podcast);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Podcast> GetAllPodcasts()
        {
            return _appDbContext.Podcasts.ToList();
        }

        public Podcast GetPodcast(int id)
        {
            return _appDbContext.Podcasts.Where(x => x.Id == id).FirstOrDefault();
        }

        public void UpdatPodcaste(Podcast podcast)
        {
            _appDbContext.Podcasts.Update(podcast);
            _appDbContext.SaveChanges();
        }
    }
}
