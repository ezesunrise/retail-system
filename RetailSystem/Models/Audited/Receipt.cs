
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailSystem.Models
{
    public class Receipt : Entity
    {
        public Receipt(): base()
        {
        }

        [Required]
        public string ReceiptNumber { get; set; }

        public int SaleId { get; set; }
        [ForeignKey(nameof(SaleId))]
        public virtual Sale Sale { get; set; }

        [StringLength(1024)]
        public string Note { get; set; }

        public string Operator { get; set; }

        public decimal CashPaid { get; set; }

    }
}