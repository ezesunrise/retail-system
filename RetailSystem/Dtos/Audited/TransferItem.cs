using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class TransferItemDto
    {
        [StringLength(512)]
        public string Description { get; set; }

        public int Quantity { get; set; }

        [StringLength(1024)]
        public string Note { get; set; }

        public int ItemId { get; set; }

        public int TransferId { get; set; }
    }
}