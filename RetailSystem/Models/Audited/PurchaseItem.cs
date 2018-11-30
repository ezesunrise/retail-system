using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Models
{
    public class PurchaseItem : Entity
    {
        [StringLength(512)]
        public string Note { get; set; }
        
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }

        public int PurchaseId { get; set; }
        public virtual Purchase Purchase { get; set; }
    }
}