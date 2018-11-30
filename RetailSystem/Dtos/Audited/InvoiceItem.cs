
using RetailSystem.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailSystem.Dtos
{
    public class InvoiceItemDto
    {
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public int InvoiceId { get; set; }
        
        [Required]
        public int Quantity { get; set; }

        public decimal TotalPrice { get => Item.UnitPrice .Value * Quantity; }
    }
}