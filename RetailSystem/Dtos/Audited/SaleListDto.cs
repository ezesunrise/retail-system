using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class SaleListDto : EntityDto
    {
        public string ReferenceNumber { get; set; }

        public decimal Total
        {
            get
            {
                var total = 0M;
                foreach (var item in this.SaleItems)
                {
                    total += item.ItemTotal;
                }
                return total;
            }
        }

        [StringLength(1024)]
        public string Note { get; set; }

        public int LocationId { get; set; }

        public ICollection<SaleItemDto> SaleItems { get; set; }
    }
}