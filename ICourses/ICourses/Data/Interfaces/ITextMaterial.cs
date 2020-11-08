using ICourses.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Interfaces
{
    public interface ITextMaterial
    {
        void AddTextMaterial(TextMaterial text);
        IEnumerable<TextMaterial> GetAllTextMaterials();
        void DeleteTextMaterial(TextMaterial text);
        void DeleteTextMaterialById(Guid id);
        TextMaterial GetTextMaterial(Guid id);
        void UpdateTextMaterial(TextMaterial text);
    }
}
