using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class Order 
    {
        public string OrderNumber { get; set; }

        public string Description { get; set; }

        [StringLength(1024)]
        public string Note { get; set; }

        public int Status { get; set; }
        public IList<KeyValuePairDto> Statuses { get; set; }

        public DateTime IssueDate { get; set; }
        public DateTime? ExpectedReceiptDate { get; set; }

        public int? PurchaseId { get; set; }

        public int LocationId { get; set; }
        public IList<KeyValuePairDto> Locations { get; set; }

    }
}