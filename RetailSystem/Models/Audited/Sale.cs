using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Models
{
    public class Sale : AuditedEntity
    {
        public Sale() : base()
        {
            SaleItems = new HashSet<SaleItem>();
        }

        [Required]
        public string ReferenceNumber { get; set; }

        [StringLength(1024)]
        public string Description { get; set; }

        public decimal Total { get; set; }

        [StringLength(1024)]
        public string Note { get; set; }

        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        public virtual ICollection<SaleItem> SaleItems { get; set; }
    }
}