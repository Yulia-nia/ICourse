using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


namespace ICourses.ViewModel
{
    public class CreateCourseViewModel
    {
        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Язык")]
        public string Language { get; set; }

        [Display(Name = "Любимое")]
        public bool IsFavorite { get; set; }

        public string SubjectName { get; set; }
    }
}
