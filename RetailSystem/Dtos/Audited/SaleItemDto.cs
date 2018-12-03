using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class SaleItemDto
    {
        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal ItemTotal { get => Quantity * UnitPrice; }

        public int ItemId { get; set; }

        public int SaleId { get; set; }
    }
}