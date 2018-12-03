using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Models
{
    public class SaleItem
    {
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public int SaleId { get; set; }
        public virtual Sale Sale { get; set; }
    }
}