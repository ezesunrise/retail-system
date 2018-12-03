using RetailSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class SupplyListDto : EntityDto
    {
        public string SupplyNumber { get; set; }

        public string Description { get; set; }

        [StringLength(1024)]
        public string Note { get; set; }

        public OrderStatus OrderStatus { get; set; }
        public string OrderStatusName { get => OrderStatus.ToString(); }

        public int PaymentStatus { get; set; }
        public string PaymentStatusName { get => PaymentStatus.ToString(); }

        public DateTime? ExpectedDeliveryDate { get; set; }

        public int LocationId { get; set; }
        public string LocationName { get; set; }

        public virtual ICollection<SupplyItemDto> SupplyItems { get; set; }
    }
}