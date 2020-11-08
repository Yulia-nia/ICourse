using ICourses.Data.Interfaces;
using ICourses.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Repositories
{
    public class VideoRepository : IVideo
    {
        private readonly AppDbContext _appDbContext;

        public VideoRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public void AddVideo(Video video)
        {
            _appDbContext.Videos.Add(video);
            _appDbContext.SaveChanges();
        }

        public void DeleteVideo(Video video)
        {
            _appDbContext.Videos.Remove(video);
            _appDbContext.SaveChanges();
        }

        public void DeleteVideoById(Guid id)
        {
            var video = _appDbContext.Videos.Where(x => x.Id == id).FirstOrDefault();
            _appDbContext.Videos.Remove(video);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Video> GetAllVideos()
        {
            return _appDbContext.Videos.ToList();
        }

        public Video GetVideo(Guid id)
        {
            return _appDbContext.Videos.Where(x => x.Id == id).FirstOrDefault();
        }

        public void UpdateVideo(Video video)
        {
            _appDbContext.Videos.Update(video);
            _appDbContext.SaveChanges();
        }
    }
}
