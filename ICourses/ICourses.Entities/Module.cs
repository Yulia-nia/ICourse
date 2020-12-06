using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Entities
{ 
    public class Module : BaseClass
    {

        public DateTime Modified { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public Course Course { get; set; }
        public Guid CourseId { get; set; }


        public virtual ICollection<TextMaterial> TextMaterials { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
    }
}
