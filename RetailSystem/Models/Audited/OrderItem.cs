﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Models
{
    public class OrderItem : Entity
    {
        public int Quantity { get; set; }

        [StringLength(512)]
        public string Note { get; set; }

        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}