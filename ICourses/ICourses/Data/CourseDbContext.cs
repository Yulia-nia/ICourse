using ICourses.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Identity;


namespace ICourses.Data
{
    public class CourseDbContext : IdentityDbContext<User>
    {
        public CourseDbContext(DbContextOptions<CourseDbContext> options) : base(options)
        {
            //Database.Migrate();
            Database.EnsureCreated();
        }
      
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<TextMaterial> TextMaterials {get; set;}
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes {get; set;}
        public override DbSet<User> Users { get; set; }
    }
}
