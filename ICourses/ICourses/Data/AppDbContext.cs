using ICourses.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            //Database.Migrate();
        }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<TextMaterial> TextMaterials {get; set;}
        public DbSet<Podcast> Podcasts {get; set;}
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes {get; set;}
        public DbSet<Image> Images { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
