using Microsoft.AspNetCore.Identity;
using RetailSystem.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Models
{
    public class AppUser : IdentityUser
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

        public int LocationId { get; set; }
        public Location Location { get; set; }

        public byte[] PasswordSalt { get; set; }
    }
}