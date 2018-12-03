using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class SaleListDto : EntityDto
    {
        [Required]
        public string ReferenceNumber { get; set; }

        [StringLength(1024)]
        public string Description { get; set; }

        public decimal Total { get; set; }

        [StringLength(1024)]
        public string Note { get; set; }

        public int LocationId { get; set; }

        public ICollection<SaleItemDto> SaleItems { get; set; }
    }
}