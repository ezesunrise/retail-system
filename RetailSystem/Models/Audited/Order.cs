using RetailSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Models
{
    public class Order : AuditedEntity
    {
        public Order() : base()
        {
            OrderItems = new HashSet<OrderItem>();
            IssueDate = DateTime.Now;
            Status = OrderStatus.Pending;
        }

        public string OrderNumber { get; set; }

        public string Description { get; set; }

        [StringLength(1024)]
        public string Note { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime? ExpectedReceiptDate { get; set; }

        public int? PurchaseId { get; set; }
        public virtual Purchase Purchase { get; set; }

        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}