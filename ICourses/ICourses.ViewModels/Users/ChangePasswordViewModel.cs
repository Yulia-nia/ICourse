using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ICourses.Users.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

    
        public string NewPassword { get; set; }

        public string OldPassword { get; set; }

    }
}
