﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Models
{
    public class Sale : AuditedEntity
    {
        public Sale() : base()
        {
            ReferenceNumber = DateTime.Now.ToFileTime().ToString();
            SaleItems = new HashSet<SaleItem>();
        }

        [Required]
        public string ReferenceNumber { get; set; }
        
        [StringLength(512)]
        public string Note { get; set; }

        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

        public virtual ICollection<SaleItem> SaleItems { get; set; }
    }
}