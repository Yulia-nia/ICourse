using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Entities
{
    public class Comment : BaseClass
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public string UserId { get; set; }
        public Course Course { get; set; }
        public Guid CourseId { get; set; }
        public DateTime Modified { get; set; } = DateTime.Now;
    }
}
