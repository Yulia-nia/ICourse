using ICourses.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Interfaces
{
    public interface ITextMaterial
    {
        Task AddTextMaterial(TextMaterial text);
        Task<IEnumerable<TextMaterial>> GetAllTextMaterials(Guid moduleId);
        Task DeleteTextMaterial(TextMaterial text);
        Task DeleteTextMaterialById(Guid id);
        Task<TextMaterial> GetTextMaterial(Guid id);
        Task UpdateTextMaterial(TextMaterial text);
    }
}
