using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class SupplierDto
    {
        [Required]
        public string SupplierCode { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        
        public string EmailAddress { get; set; }

        [StringLength(128)]
        public string ContactPerson { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string AlternatePhoneNumber { get; set; }

        public string Address { get; set; }

        public int BusinessId { get; set; }
        
    }
}