using ICourses.Entities;
using ICourses.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Services.Interfaces
{
    public interface IModuleService
    {
        /// <summary>
        /// Получаем модуль
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Module> GetModule(Guid id);

        /// <summary>
        /// Добавляем новый модуль
        /// </summary>
        /// <param name="id"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        Task<Module> AddModule(Guid id, CreateModuleViewModel module);

        /// <summary>
        /// Редактирование
        /// </summary>
        /// <param name="id"></param>
        /// <param name="module"></param>
        /// <returns></returns>
        Task<Module> EditModule(Guid id, ChangeModuleViewModel module);

        /// <summary>
        /// Получить все текстовые материалы модуля
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>        
       //Task<IEnumerable<TextMaterial>> GetAllTextMaterials(Guid id);

        /// <summary>
        /// Получить все видео модуля
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //Task<IEnumerable<Video>> GetAllVideos(Guid id);

        /// <summary>
        /// Получить все модули (курса)
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Module>> GetAllModules(); // переписать

        /// <summary>
        /// Удалить модуль
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteModuleById(Guid id);

        /// <summary>
        /// обновить модуль
        /// </summary>
        /// <param name="module"></param>
        /// <returns></returns>
        Task UpdateModule(Module module);
      
    }
}
