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
            AppUsers = new HashSet<AppUser>();
            Status = Status.Active;
            LocationItems = new HashSet<LocationItem>();
            Sales = new HashSet<Sale>();
            OutgoingTransfers = new HashSet<Transfer>();
            IncomingTransfers = new HashSet<Transfer>();
            PurchaseOrders = new HashSet<PurchaseOrder>();
            Invoices = new HashSet<Invoice>();
            Users = new HashSet<IdentityUser>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 5)]
        public string Code { get; set; }

        public LocationType Type { get; set; }
        public Status Status { get; set; }

        public decimal? Target { get; set; }
        public TargetType? TargetType { get; set; }

        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber1 { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber2 { get; set; }

        public string ContactPerson { get; set; }
        
        public string AdditionalInfo { get; set; }

        public int BusinessId { get; set; }
        public Business Business { get; set; }

        public virtual ICollection<AppUser> AppUsers { get; set; }
        public ICollection<LocationItem> LocationItems { get; set; }
        public ICollection<Sale> Sales { get; set; }
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        public ICollection<IdentityUser> Users { get; set; }
        
        public ICollection<Transfer> OutgoingTransfers { get; set; }
        public ICollection<Transfer> IncomingTransfers { get; set; }
    }
}