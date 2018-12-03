using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class SupplyItemDto
    {
        [StringLength(512)]
        public string Note { get; set; }
        
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        [Range(0, int.MaxValue)]
        public int QuantityDelivered { get; set; }

        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        public int ItemId { get; set; }
        public int SupplyId { get; set; }
    }
}