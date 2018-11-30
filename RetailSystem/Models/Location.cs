using Microsoft.AspNetCore.Identity;
using RetailSystem.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailSystem.Models
{
    public class Location : Entity
    {
        public Location()
        {
            Status = LocationStatus.Active;
            LocationItems = new HashSet<LocationItem>();
            Sales = new HashSet<Sale>();
            Orders = new HashSet<Order>();
            OutgoingTransfers = new HashSet<Transfer>();
            IncomingTransfers = new HashSet<Transfer>();
            Purchases = new HashSet<Purchase>();
            Invoices = new HashSet<Invoice>();
            Users = new HashSet<IdentityUser>();
        }
        
        public string Name { get; set; }

        public LocationType Type { get; set; }
        public LocationStatus Status { get; set; }

        public decimal? Target { get; set; }
        public TargetType? TargetType { get; set; }

        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string AlternatePhoneNumber { get; set; }

        public string ContactPerson { get; set; }

        public int BusinessId { get; set; }
        public Business Business { get; set; }

        public ICollection<LocationItem> LocationItems { get; set; }
        public ICollection<Sale> Sales { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Purchase> Purchases { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        public ICollection<IdentityUser> Users { get; set; }
        
        public ICollection<Transfer> OutgoingTransfers { get; set; }
        public ICollection<Transfer> IncomingTransfers { get; set; }
    }
}