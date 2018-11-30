using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Models
{
    public class SaleItem : Entity
    {
        [StringLength(512)]
        public string Description { get; set; }

        [StringLength(512)]
        public string Note { get; set; }
        
        public uint Quantity { get; set; }
        
        public decimal UnitPrice { get; set; }

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public int SaleId { get; set; }
        public virtual Sale Sale { get; set; }
    }
}