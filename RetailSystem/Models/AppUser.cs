using Microsoft.AspNetCore.Identity;
using RetailSystem.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Models
{
    public class AppUser : IdentityUser<int>
    {
        public AppUser()
        {
        }
        
        [Required]
        [StringLength(64)]
        public string Name { get; set; }
        
        [StringLength(256)]
        public string Address { get; set; }

        public Status Status { get; set; }

        public byte[] Photo { get; set; }

        public int? LocationId { get; set; }
        public virtual Location Location { get; set; }

        public int? BusinessId { get; set; }
        public virtual Business Business { get; set; }

        [Required]
        public string PasswordSalt { get; set; }

        public string Role { get; set; }

        public string Token { get; set; }
    }
}