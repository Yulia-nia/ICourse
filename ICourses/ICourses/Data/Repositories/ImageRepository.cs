using ICourses.Data.Interfaces;
using ICourses.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Repositories
{
    public class ImageRepository : IImage
    {
        private readonly AppDbContext _appDbContext;

        public ImageRepository(AppDbContext appDbContext)
        {
            this._appDbContext = appDbContext;
        }

        public void AddImage(Image image)
        {
            _appDbContext.Images.Add(image);
            _appDbContext.SaveChanges();
        }

        public void DeleteImage(Image image)
        {
            _appDbContext.Images.Remove(image);
            _appDbContext.SaveChanges();
        }

        public void DeleteImageById(Guid id)
        {
            var image = _appDbContext.Images.Where(x => x.Id == id).FirstOrDefault();
            _appDbContext.Images.Remove(image);
            _appDbContext.SaveChanges();
        }

        public IEnumerable<Image> GetAllImages()
        {
            return _appDbContext.Images.ToList();
        }

        public Image GetImage(Guid id)
        {
            return _appDbContext.Images.Where(x => x.Id == id).FirstOrDefault();

        }

        public void UpdateImage(Image image)
        {
            _appDbContext.Images.Update(image);
            _appDbContext.SaveChanges();
        }
    }
}
