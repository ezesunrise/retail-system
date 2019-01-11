using RetailSystem.Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class SupplierListDto : EntityDto
    {
        [Required]
        public string SupplierNumber { get; set; }

        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        
        public string Email { get; set; }

        [StringLength(128)]
        public string ContactPerson { get; set; }
        
        public string PhoneNumber1 { get; set; }
        
        public string PhoneNumber2 { get; set; }

        public string Address { get; set; }

        public Status Status { get; set; }
        public string StatusName { get => Status.ToString(); }

        public int BusinessId { get; set; }
        public string BusinessName { get; set; }

    }
}