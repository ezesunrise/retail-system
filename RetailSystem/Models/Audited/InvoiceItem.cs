
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailSystem.Models
{
    public class InvoiceItem
    {
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public int InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
        
        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public decimal TotalPrice { get => Item.UnitPrice * Quantity; }
    }
}