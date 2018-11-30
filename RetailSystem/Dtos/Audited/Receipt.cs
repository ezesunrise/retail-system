
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RetailSystem.Dtos
{
    public class Receipt
    {
        public Receipt(): base()
        {
        }

        [Required]
        public string ReferenceNumber { get; set; }

        public int SaleId { get; set; }

        [StringLength(1024)]
        public string Note { get; set; }

        public string Operator { get; set; }

        public decimal CashPaid { get; set; }

    }
}