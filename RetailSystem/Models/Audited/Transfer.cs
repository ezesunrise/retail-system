using RetailSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailSystem.Models
{
    public class Transfer : AuditedEntity
    {
        public Transfer() : base()
        {
            Status = OrderStatus.Pending;
            TransferItems = new HashSet<TransferItem>();
        }

        [Required]
        public string ReferenceNumber { get; set; }

        [StringLength(512)]
        public string Description { get; set; }

        [StringLength(1024)]
        public string Note { get; set; }

        public OrderStatus Status { get; set; }

        [ForeignKey(nameof(SourceLocation))]
        public int SourceLocationId { get; set; }
        public virtual Location SourceLocation { get; set; }

        [ForeignKey(nameof(DestinationLocation))]
        public int DestinationLocationId { get; set; }
        public virtual Location DestinationLocation { get; set; }

        public virtual ICollection<TransferItem> TransferItems { get; set; }
    }
}