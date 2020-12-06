using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ICourses.Interfaces;
using ICourses.Services.Interfaces;
using ICourses.Entities;
using ICourses.ViewModels;

namespace ICourses.Services
{
    public class TextService : ITextService
    {
        private readonly ITextMaterial _text;
        private readonly IModuleService _moduleService;
        
        public TextService(ITextMaterial text, IModuleService moduleService)
        {
            _moduleService = moduleService;
            _text = text;
        }

        public async Task<TextMaterial> AddTextMaterial(Guid id, TextViewModel text)
        {
            Module module = await _moduleService.GetModule(id);

            TextMaterial text1 = new TextMaterial { 
                Id = Guid.NewGuid(),
                Name = text.Name,
                Context = text.Context,
                Modified = DateTime.Now,
                ModuleId = module.Id,
            };
            await _text.AddTextMaterial(text1);
            return text1;
        }

        public async Task DeleteTextMaterialById(Guid id)
        {
            await _text.DeleteTextMaterialById(id);
        }

        public async Task<TextMaterial> EditTextMaterial(Guid id, TextViewModel text)
        {
            TextMaterial textMaterial = await _text.GetTextMaterial(id);
            if (textMaterial != null)
            {
                textMaterial.Name = text.Name;
                textMaterial.Context = text.Context;
                await _text.UpdateTextMaterial(textMaterial);
            }
            return textMaterial;
        }

        public async Task<IEnumerable<TextMaterial>> GetAllTextMaterials(Guid id)
        {

            return await _text.GetAllTextMaterials(id);
        }

        public async Task<TextMaterial> GetTextMaterial(Guid id)
        {
            return await _text.GetTextMaterial(id);
        }
    }
}
