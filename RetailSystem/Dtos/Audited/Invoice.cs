
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class InvoiceDto
    {
        [Required]
        public string ReferenceNumber { get; set; }

        [StringLength(64)]
        public string Receiver { get; set; }

        [StringLength(512)]
        public string Description { get; set; }

        [StringLength(1024)]
        public string Note { get; set; }
    }
}