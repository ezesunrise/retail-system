using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Models
{
    public class TransferItem
    {
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [StringLength(256)]
        public string Note { get; set; }

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public int TransferId { get; set; }
        public virtual Transfer Transfer { get; set; }
    }
}