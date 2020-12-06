using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Entities
{
    public class Like : BaseClass
    {        
        public User User { get; set; }
        public string UserId { get; set; }

        public Course Course { get; set; }
        public Guid CourseId { get; set; }
    }
}
