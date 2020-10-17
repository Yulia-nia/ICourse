using ICourses.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Interfaces
{
    public interface IModule
    {
        void AddModule(Module module);
        IEnumerable<Module> GetAllModules();
        void DeleteModule(Module module);
        void DeleteModuleById(int id);
        Module GetModule(int id);
        void UpdateModule(Module module);

        IEnumerable<TextMaterial> GetTextMaterials(Module module);
        IEnumerable<Video> GetVideos(Module module);
        IEnumerable<Podcast> GetPodcasts(Module module);
        IEnumerable<Comment> GetComments(Module module);
    }
}
