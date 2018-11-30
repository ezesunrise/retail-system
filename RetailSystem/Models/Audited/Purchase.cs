using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Models
{
    public class Purchase : AuditedEntity
    {
        public Purchase() : base()
        {
            PurchaseItems = new HashSet<PurchaseItem>();
        }

        [Required]
        public string ReferenceNumber { get; set; }

        [StringLength(512)]
        public string Description { get; set; }

        [StringLength(1024)]
        public string Note { get; set; }

        public int? OrderId { get; set; }
        public virtual Order Order { get; set; }

        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        public virtual ICollection<PurchaseItem> PurchaseItems { get; set; }
    }
}