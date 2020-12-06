using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Entities
{
    public class Subject : BaseClass
    {        
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
