using RetailSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RetailSystem.Models
{
    public class BackOrder : AuditedEntity
    {
        public BackOrder() : base()
        {
            PurchaseOrderItems = new HashSet<PurchaseOrderItem>();
        }
        public string OrderNumber { get; set; }

        [StringLength(1024)]
        public string Note { get; set; }

        public OrderStatus OrderStatus { get; set; }
        public PaymentStatus PaymentStatus { get; set; }

        public int PurchaseOrderId { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }

        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
    }
}
