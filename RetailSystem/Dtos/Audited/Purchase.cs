using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class PurchaseDto
    {

        [Required]
        public string ReferenceNumber { get; set; }

        [StringLength(512)]
        public string Description { get; set; }

        [StringLength(1024)]
        public string Note { get; set; }

        public int? OrderId { get; set; }

        public int LocationId { get; set; }

    }
}