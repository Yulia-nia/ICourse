using ICourses.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Interfaces
{
    public interface IPodcast
    {
        Task AddPodcast(Podcast podcast);
        Task<IEnumerable<Podcast>> GetAllPodcasts();
        Task DeletePodcast(Podcast podcast);
        Task DeletePodcastById(Guid id);
        Task<Podcast> GetPodcast(Guid id);
        Task UpdatPodcaste(Podcast podcast);
    }
}
