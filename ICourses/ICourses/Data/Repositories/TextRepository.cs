using ICourses.Data.Interfaces;
using ICourses.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Repositories
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

        public async Task<IEnumerable<TextMaterial>> GetAllTextMaterials()
        {
            return await _appDbContext.TextMaterials.ToListAsync();
        }

        public async Task<TextMaterial> GetTextMaterial(Guid id)
        {
            return await _appDbContext.TextMaterials.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateTextMaterial(TextMaterial text)
        {
            _appDbContext.TextMaterials.Update(text);
            await _appDbContext.SaveChangesAsync();
        }
    }
}
