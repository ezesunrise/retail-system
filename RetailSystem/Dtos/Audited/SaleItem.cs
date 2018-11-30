using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class SaleItemDto
    {
        [StringLength(512)]
        public string Description { get; set; }

        [StringLength(512)]
        public string Note { get; set; }
        
        public uint Quantity { get; set; }
        
        public decimal UnitPrice { get; set; }

        public int ItemId { get; set; }

        public int SaleId { get; set; }
    }
}