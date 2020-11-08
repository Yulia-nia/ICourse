using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Data.Models
{
    public class Image
    {
        public Guid Id { get; set; }
        [Required]
        public string Path { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.Now;
    }
}
