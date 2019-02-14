using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RetailSystem.Models
{
    public class Item : Entity
    {
        public Item()
        {
            LocationItems = new HashSet<LocationItem>();
            SaleItems = new HashSet<SaleItem>();
            PurchaseOrderItems = new HashSet<PurchaseOrderItem>();
            TransferItems = new HashSet<TransferItem>();
            InvoiceItems = new HashSet<InvoiceItem>();
        }

        [Required]
        [StringLength(6, MinimumLength = 6, ErrorMessage ="Code must be {0} long")]
        public string Code { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal UnitCost { get; set; }
        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        [Range(0, 100)]
        public byte Tax { get; set; }
        
        public int UnitId { get; set; }
        public virtual Unit Unit { get; set; }

        public byte[] Photo { get; set; }
        public byte[] AlternatePhoto { get; set; }

        [StringLength(1024)]
        public string Note { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public int? SubCategoryId { get; set; }
        public virtual SubCategory SubCategory { get; set; }

        public int? ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }

        public int? SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }
        
        public string AdditionalInfo { get; set; }

        public virtual ICollection<LocationItem> LocationItems { get; set; }
        public virtual ICollection<SaleItem> SaleItems { get; set; }
        public virtual ICollection<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public virtual ICollection<TransferItem> TransferItems { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
