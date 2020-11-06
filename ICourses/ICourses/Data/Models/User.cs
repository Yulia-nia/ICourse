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
        //[Key]
        //public int Id { get; set; }
        public int Year { get; set; }
        public virtual ICollection<Course> Courses { get; set; }

    }
}
