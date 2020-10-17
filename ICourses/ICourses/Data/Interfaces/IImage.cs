using ICourses.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Interfaces
{
    public interface IImage
    {
        void AddImage(Image image);
        IEnumerable<Image> GetAllImages();
        void DeleteImage(Image image);
        void DeleteImageById(int id);
        Image GetImage(int id);
        void UpdateImage(Image image);
    }
}
