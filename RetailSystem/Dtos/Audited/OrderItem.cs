using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class OrderItemDto
    {
        public int Quantity { get; set; }

        [StringLength(512)]
        public string Note { get; set; }

        public int ItemId { get; set; }

        public int OrderId { get; set; }
    }
}