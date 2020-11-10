using ICourses.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Interfaces
{
    public interface IModule
    {
        Task AddModule(Module module);
        Task<IEnumerable<Module>> GetAllModules();
        Task DeleteModule(Module module);
        Task DeleteModuleById(Guid id);
        Task<Module> GetModule(Guid id);
        Task UpdateModule(Module module);

        IEnumerable<TextMaterial> GetTextMaterials(Module module);
        IEnumerable<Video> GetVideos(Module module);
        IEnumerable<Podcast> GetPodcasts(Module module);
        //IEnumerable<Comment> GetComments(Module module);
    }
}
