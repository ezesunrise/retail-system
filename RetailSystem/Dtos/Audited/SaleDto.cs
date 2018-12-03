using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class SaleDto : EntityDto
    {
        [Required]
        public string ReferenceNumber { get; set; }

        public decimal Total { get {
                var total = 0M;
                foreach(var item in this.SaleItems)
                {
                    total += item.ItemTotal;
                }
                return total;
            }
        }

        [StringLength(512)]
        public string Note { get; set; }

        public int LocationId { get; set; }

        public ICollection<SaleItemDto> SaleItems { get; set; }
    }
}