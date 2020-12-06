using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ICourses.Entities
{
    public class User : IdentityUser
    {       
        public int Year { get; set; }
        public byte[] Avatar { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

    }
}
