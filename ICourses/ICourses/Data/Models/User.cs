using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Models
{
    public class User : IdentityUser
    {
       
        public int Year { get; set; }
        public byte[] Avatar { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

    }
}
