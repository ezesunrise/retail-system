using Microsoft.AspNetCore.Identity;
using RetailSystem.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Models
{
    public class Business : Entity
    {
        public Business()
        {
            AppUsers = new HashSet<AppUser>();
            Locations = new HashSet<Location>();
            Customers = new HashSet<Customer>();
            Suppliers = new HashSet<Supplier>();
            Manufacturers = new HashSet<Manufacturer>();
            Units = new HashSet<Unit>();
        }

        [Required]
        [StringLength(64)]
        public string Name { get; set; }
        
        [StringLength(256)]
        public string Description { get; set; }

        public Status Status { get; set; }

        public byte[] Logo { get; set; }

        public virtual ICollection<AppUser> AppUsers { get; set; }
        public virtual ICollection<Location> Locations { get; set; }
        public virtual ICollection<Manufacturer> Manufacturers { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Supplier> Suppliers { get; set; }
        public virtual ICollection<Unit> Units { get; set; }
    }
}