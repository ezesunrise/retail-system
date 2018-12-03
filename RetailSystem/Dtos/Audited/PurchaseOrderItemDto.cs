using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class PurchaseOrderItemDto
    {

        [StringLength(512)]
        public string Note { get; set; }

        public int Quantity { get; set; }
        public int QuantityDelivered { get; set; }
        public int FaultQuantity { get; set; }

        public decimal UnitCost { get; set; }
        public decimal ItemTotal { get => Quantity * UnitCost; }


        public int ItemId { get; set; }
        public string ItemName { get; set; }

        public int PurchaseOrderId { get; set; }
        public string PurchaseOrderName { get; set; }
    }
}