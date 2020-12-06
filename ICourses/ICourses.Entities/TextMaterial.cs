using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Entities
{
    public class TextMaterial : BaseClass
    {
        public string Name { get; set; }
        public string Context { get; set; }
        public Module Module { get; set; }
        public Guid ModuleId { get; set; }
        public DateTime Modified { get; set; } = DateTime.Now;
    }
}
