using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Entities
{
    public class Course : BaseClass
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Modified { get; set; } = DateTime.Now;
        public byte[] Image { get; set; }
        public string Language { get; set; }
        
        public User Author { get; set; }
        public string AuthorId { get; set; }
       
        //public bool IsFavorite { get; set; }
        //public string UserId { get; set; }

        public Subject Subject { get; set; }
        public Guid SubjectId { get; set; }
        
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Module> Modules { get; set; }
       

    }
}
