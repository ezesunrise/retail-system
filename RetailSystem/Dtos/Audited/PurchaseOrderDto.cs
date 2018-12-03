using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RetailSystem.Models.Enums;

namespace RetailSystem.Dtos
{
    public class PurchaseOrderDto : EntityDto
    {
        
        public string OrderNumber { get; set; }

        public string Description { get; set; }

        [StringLength(1024)]
        public string Note { get; set; }

        public int OrderStatus { get; set; }
        public IList<KeyValuePairDto> OrderStatusList { get
            {
                IList<KeyValuePairDto> list = new List<KeyValuePairDto>();
                foreach (int value in Enum.GetValues(typeof(OrderStatus)))
                {
                    list.Add(new KeyValuePairDto { Value = value, DisplayName = Enum.GetName(typeof(OrderStatus), value) });
                }
                return list;
            }
        }

        public int PaymentStatus { get; set; }
        public IList<KeyValuePairDto> PaymentStatusList
        {
            get
            {
                IList<KeyValuePairDto> list = new List<KeyValuePairDto>();
                foreach (int value in Enum.GetValues(typeof(PaymentStatus)))
                {
                    list.Add(new KeyValuePairDto { Value = value, DisplayName = Enum.GetName(typeof(PaymentStatus), value) });
                }
                return list;
            }
        }
        
        public DateTime IssueDate { get; set; }
        public DateTime? ExpectedReceiptDate { get; set; }

        public int LocationId { get; set; }
        
        public ICollection<PurchaseOrderItemDto> PurchaseOrderItems { get; set; }
    }
}