using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ICourses.Entities
{
    public class CourseDbContext : IdentityDbContext<User>
    {
        public CourseDbContext(DbContextOptions<CourseDbContext> options) : base(options)
        {
            //Database.Migrate();
            Database.EnsureCreated();
            //Database.EnsureCreated();
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
