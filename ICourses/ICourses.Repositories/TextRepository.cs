using ICourses.Interfaces;
using ICourses.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Repositories
{
    public class TextRepository : ITextMaterial
    {
        private readonly CourseDbContext _appDbContext;

        public TextRepository(CourseDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public async Task AddTextMaterial(TextMaterial text)
        {
            await _appDbContext.TextMaterials.AddAsync(text);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteTextMaterial(TextMaterial text)
        {
            _appDbContext.TextMaterials.Remove(text);
            await  _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteTextMaterialById(Guid id)
        {
            var text = await _appDbContext.TextMaterials.Where(x => x.Id == id).FirstOrDefaultAsync();
            _appDbContext.TextMaterials.Remove(text);
            await _appDbContext.SaveChangesAsync();
        }

        //получаем айди модуля и выводим все его материалы
        public async Task<IEnumerable<TextMaterial>> GetAllTextMaterials(Guid moduleId)
        {
            var module = await _appDbContext.Modules.Where(_ => _.Id == moduleId).FirstOrDefaultAsync();
            return module.TextMaterials.ToList();
        }

        public async Task<TextMaterial> GetTextMaterial(Guid id)
        {
            return await _appDbContext.TextMaterials.Where(x => x.Id == id).Include(t => t.Module).FirstOrDefaultAsync();
        }

        public async Task UpdateTextMaterial(TextMaterial text)
        {
            _appDbContext.TextMaterials.Update(text);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
