using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Models
{
    public class User : IdentityUser
    {
        public string Description { get; set; }
        public Image Avatar { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }

    }
}
