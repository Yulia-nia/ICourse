using ICourses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Interfaces
{
    public interface IModule
    {
        Task AddModule(Module module);
        Task<IEnumerable<Module>> GetAllModules();
        Task DeleteModule(Module module);
        Task DeleteModuleById(Guid id);
        Task<Module> GetModule(Guid id);
        Task UpdateModule(Module module);

       
        //IEnumerable<Comment> GetComments(Module module);
    }
}
