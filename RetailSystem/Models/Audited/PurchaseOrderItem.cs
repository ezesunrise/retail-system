using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Models
{
    public class PurchaseOrderItem
    {
        [StringLength(512)]
        public string Note { get; set; }
        
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        [Range(0, int.MaxValue)]
        public int QuantityDelivered { get; set; }
        [Range(0, int.MaxValue)]
        public int FaultQuantity { get; set; }

        [Range(0, double.MaxValue)]
        public decimal UnitCost { get; set; }

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public int PurchaseOrderId { get; set; }
        public virtual PurchaseOrder PurchaseOrder { get; set; }
    }
}