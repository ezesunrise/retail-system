using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class PurchaseItemDto
    {
        [StringLength(512)]
        public string Note { get; set; }
        
        public int ItemId { get; set; }

        public int PurchaseId { get; set; }
    }
}