using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Models

{
    public class Podcast
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public string Content { get; set; }
        public Module Module { get; set; }
        public int ModuleId { get; set; }
    }
}
