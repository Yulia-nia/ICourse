using ICourses.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Interfaces
{
    public interface IPodcast
    {
        void AddPodcast(Podcast podcast);
        IEnumerable<Podcast> GetAllPodcasts();
        void DeletePodcast(Podcast podcast);
        void DeletePodcastById(int id);
        Podcast GetPodcast(int id);
        void UpdatPodcaste(Podcast podcast);
    }
}
