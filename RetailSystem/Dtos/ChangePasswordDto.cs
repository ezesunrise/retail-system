using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RetailSystem.Dtos
{
    public class ChangePasswordDto
    {
        [Required]
        public string CurrentPassword { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 5)]
        public string NewPassword { get; set; }
    }
}