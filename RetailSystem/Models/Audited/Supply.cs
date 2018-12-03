using RetailSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Models
{
    public class Supply : AuditedEntity
    {
        public string ReferenceNumber { get; set; }

        [StringLength(1024)]
        public string Note { get; set; }

        public OrderStatus OrderStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        public DateTime? ExpectedDeliveryDate { get; set; }

        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        public virtual ICollection<SupplyItem> SupplyItems { get; set; }
    }
}