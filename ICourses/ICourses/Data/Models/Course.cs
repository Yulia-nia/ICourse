using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Modified { get; set; } = DateTime.Now;
        
        // public bool? IsFree { get; set; }
        // public double? Cost { get; set; }

        public Image Image { get; set; }
        public string Language { get; set; }
        
        //public User Author { get; set; }
        //public int AuthorID { get; set; }
       
        public bool IsFavorite { get; set; }
        public Subject Subject { get; set; }
        public int SubjectID { get; set; }
        
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Module> Modules { get; set; }
       

    }
}
