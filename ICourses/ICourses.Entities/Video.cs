using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Entities
{
    public class Video : BaseClass 
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public Module Module {get; set;}
        public Guid Moduleid { get; set; }
        
    }
}
