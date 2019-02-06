using Microsoft.AspNetCore.Identity;
using RetailSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class AuthDto
    {
        [Required]
        [StringLength(64)]
        public string UserName { get; set; }
        
        [Required]
        [StringLength(64)]
        public string Password { get; set; }
    }
}