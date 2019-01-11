using RetailSystem.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Models
{
    public class Supplier : Entity
    {
        public Supplier()
        {
            Items = new HashSet<Item>();
        }

        [Required]
        [StringLength(6,MinimumLength = 6)]
        public string SupplierNumber { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        
        public string AdditionalInfo { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [StringLength(128)]
        public string ContactPerson { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber1 { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber2 { get; set; }

        public string Address { get; set; }

        public Status Status { get; set; }

        public int BusinessId { get; set; }
        public Business Business { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}