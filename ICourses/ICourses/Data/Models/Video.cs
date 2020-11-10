using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Models
{
    public class Video
    {

        [Required]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public Module Module {get; set;}
        public Guid Moduleid { get; set; }
        
    }
}
