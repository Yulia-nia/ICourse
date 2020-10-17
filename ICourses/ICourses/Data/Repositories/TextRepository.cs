using ICourses.Data.Interfaces;
using ICourses.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Repositories
{
    public class TextRepository : ITextMaterial
    {
        private readonly AppDbContext _appDbContext;

        public TextRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }
        public void AddTextMaterial(TextMaterial text)
        {
            _appDbContext.TextMaterials.Add(text);
            _appDbContext.SaveChanges();
        }

        public void DeleteTextMaterial(TextMaterial text)
        {
            _appDbContext.TextMaterials.Remove(text);
            _appDbContext.SaveChanges();
        }

        public void DeleteTextMaterialById(int id)
        {
            var text = _appDbContext.TextMaterials.Where(x => x.Id == id).FirstOrDefault();
            _appDbContext.TextMaterials.Remove(text);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<TextMaterial> GetAllTextMaterials()
        {
            return _appDbContext.TextMaterials.ToList();
        }

        public TextMaterial GetTextMaterial(int id)
        {
            return _appDbContext.TextMaterials.Where(x => x.Id == id).FirstOrDefault();
        }

        public void UpdateTextMaterial(TextMaterial text)
        {
            _appDbContext.TextMaterials.Update(text);
            _appDbContext.SaveChanges();
        }
    }
}
