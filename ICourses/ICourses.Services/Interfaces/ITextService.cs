using ICourses.Entities;
using ICourses.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Services.Interfaces
{
    public interface ITextService
    {
        Task<TextMaterial> AddTextMaterial(Guid id, TextViewModel text);
        Task<TextMaterial> EditTextMaterial(Guid id, TextViewModel text);

        Task<IEnumerable<TextMaterial>> GetAllTextMaterials(Guid id);
        Task DeleteTextMaterialById(Guid id);

        Task<TextMaterial> GetTextMaterial(Guid id);
       
    }
}
