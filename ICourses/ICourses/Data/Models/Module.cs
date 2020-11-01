using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Models
{
    public class Module
    {
        [Key]
        public int Id { get; set; }
        public DateTime Modified { get; set; } = DateTime.Now;
        public string Name { get; set; }
        public string Description { get; set; }
        public Image Image { get; set; }
        public Course Course { get; set; }
        public int CourseId { get; set; }
        public virtual ICollection<TextMaterial> TextMaterials { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
        public virtual ICollection<Podcast> Podcasts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
