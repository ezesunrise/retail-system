using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class TransferItemDto
    {
        public int Quantity { get; set; }

        [StringLength(256)]
        public string Note { get; set; }

        public int ItemId { get; set; }

        public int TransferId { get; set; }
    }
}