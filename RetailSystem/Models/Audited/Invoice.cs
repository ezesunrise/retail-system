
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Models
{
    public class Invoice : AuditedEntity
    {
        public Invoice(): base()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
        }

        [Required]
        public string InvoiceNumber { get; set; }

        [StringLength(64)]
        public string Receiver { get; set; }

        [StringLength(1024)]
        public string Note { get; set; }

        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}