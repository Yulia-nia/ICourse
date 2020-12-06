using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ICourses.Interfaces;
using ICourses.Services.Interfaces;
using ICourses.Entities;
using ICourses.ViewModels;

namespace ICourses.Services
{
    public class ModuleService : IModuleService
    {

        private readonly IModule _module;
        //private readonly ITextService _textService;
        //private readonly IVideoService _videoService;
        private readonly ICourseService _courseService;

        public ModuleService(IModule module/*, IVideoService videoService, ITextService textService*/, ICourseService courseService)
        {
        
            _module = module;    
            _courseService = courseService;
            //_textService = textService;
            //_videoService = videoService;
        }

        public async Task<Module> AddModule(Guid id, CreateModuleViewModel module)
        {
            byte[] imageData = null;

            using (var binaryReader = new BinaryReader(module.Image.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)module.Image.Length);
            }

            Course course = await _courseService.GetCourse(id);
            Module new_module = new Module
            {
                Id = Guid.NewGuid(),
                Name = module.Name,
                Description = module.Description,
                Image = imageData,
                Modified = DateTime.Now,
                CourseId =course.Id,
              };
             await _module.AddModule(new_module);
             return new_module;  
        }

        public async Task DeleteModuleById(Guid id)
        {           
            //Module module = await _module.GetModule(id);

            //foreach(var item in module.TextMaterials)
            //{
            //    await _textService.DeleteTextMaterialById(item.Id);
            //}
            //foreach (var item in module.Videos)
            //{
            //    await _videoService.DeleteVideoById(item.Id);
            //}           
           
            await _module.DeleteModuleById(id);
        }

        public async Task<Module> EditModule(Guid id, ChangeModuleViewModel module)
        {
            byte[] imageData = null;

            using (var binaryReader = new BinaryReader(module.Image.OpenReadStream()))
            {
                imageData = binaryReader.ReadBytes((int)module.Image.Length);
            }

            Module new_module = await _module.GetModule(id);

            if (new_module != null)
            {
                new_module.Name = module.Name;
                new_module.Description = module.Description;
                new_module.Image = imageData;
                await _module.UpdateModule(new_module);
                return new_module;
            }
            return null;
        }

        public async Task<IEnumerable<Module>> GetAllModules()
        {
            return await _module.GetAllModules();
        }

        //// Получаем все материалы модуля
        //public async Task<IEnumerable<TextMaterial>> GetAllTextMaterials(Guid id)
        //{
        //    return await _textService.GetAllTextMaterials(id);
        //}

        //public async Task<IEnumerable<Video>> GetAllVideos(Guid id)
        //{
        //    return await _videoService.GetAllVideos(id);
        //}

        public async Task<Module> GetModule(Guid id)
        {
            return await _module.GetModule(id);
        }

        public async Task UpdateModule(Module module)
        {
            await _module.UpdateModule(module);
        }
    }
}
