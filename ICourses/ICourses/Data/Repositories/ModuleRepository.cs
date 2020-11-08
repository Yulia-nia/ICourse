using ICourses.Data.Interfaces;
using ICourses.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Repositories
{
    public class ModuleRepository : IModule
    {
        private readonly AppDbContext _appDbContext;

        public ModuleRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public void AddModule(Module module)
        {
            _appDbContext.Modules.Add(module);
            _appDbContext.SaveChanges();
        }

        public void DeleteModule(Module module)
        {
            _appDbContext.Modules.Remove(module);
            _appDbContext.SaveChanges();
        }

        public void DeleteModuleById(Guid id)
        {
            var module = _appDbContext.Modules.Where(x => x.Id == id).FirstOrDefault();
            _appDbContext.Modules.Remove(module);
            _appDbContext.SaveChanges();
        }

        public Module GetModule(Guid id)
        {
            return _appDbContext.Modules.Where(x => x.Id == id).FirstOrDefault();
        }

        public IEnumerable<Module> GetAllModules()
        {
            return _appDbContext.Modules.ToList();
        }

        public void UpdateModule(Module module)
        {
            _appDbContext.Modules.Update(module);
            _appDbContext.SaveChanges();
        }

        //public int GetCourseId(string name)
        //{
        //    _appDbContext.Modules.Where(_ => Equals(_.Course.Name, name));
        //    return id;
        //}

        public Guid GetCourseId(string name)
        {
            return _appDbContext.Modules.FirstOrDefault(_ => Equals(_.Course.Name, name)).Id;
        }


        public IEnumerable<TextMaterial> GetTextMaterials(Module module)
        {
            var text = _appDbContext.Modules.Where(c => c.Id == module.Id)?.SelectMany(c => c.TextMaterials).ToList();
            return text.AsReadOnly(); ;
        }
        public IEnumerable<Video> GetVideos(Module module)
        {
            var videos = _appDbContext.Modules.Where(c => c.Id == module.Id)?.SelectMany(c => c.Videos).ToList();
            return videos.AsReadOnly(); ;
        }
        public IEnumerable<Podcast> GetPodcasts(Module module)
        {
            var podcasts = _appDbContext.Modules.Where(c => c.Id == module.Id)?.SelectMany(c => c.Podcasts).ToList();
            return podcasts.AsReadOnly(); ;
        }

        //public IEnumerable<Comment> GetComments(Module module)
        //{
        //    var comment = _appDbContext.Modules.Where(c => c.Id == module.Id)?.SelectMany(c => c.Comments).ToList();
        //    return comment.AsReadOnly();
        //}



        /*public void AddComment(string userId, Comment comment)
        {
            var user = _appDbContext.Users.FirstOrDefault(c => c.Id.Equals(userId));
            
            _appDbContext.Comments.Add(comment);
            _appDbContext.SaveChanges();
           
        }*/
    }
}
