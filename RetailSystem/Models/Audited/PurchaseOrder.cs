using RetailSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Models
{
    public class PurchaseOrder : AuditedEntity
    {
        public PurchaseOrder() : base()
        {
            IssueDate = DateTime.Now;
            PurchaseOrderItems = new HashSet<PurchaseOrderItem>();
        }
        public string OrderNumber { get; set; }

        public string Description { get; set; }

        [StringLength(1024)]
        public string Note { get; set; }

        public OrderStatus OrderStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime? ExpectedReceiptDate { get; set; }

        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    }
}