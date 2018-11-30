using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RetailSystem.Dtos
{
    public class ItemListDto
    {
        [Required]
        [StringLength(6, MinimumLength = 6, ErrorMessage ="Code must be {0} long")]
        public string Code { get; set; }

        [Required]
        [StringLength(256)]
        public string Description { get; set; }
        
        public decimal UnitCost { get; set; }
        public decimal? UnitPrice { get; set; }

        public byte? Tax { get; set; }
        
        public int UnitId { get; set; }

        public byte[] Photo { get; set; }
        public byte[] AlternatePhoto { get; set; }

        [StringLength(1024)]
        public string Note { get; set; }

        public int? CategoryId { get; set; }

        public int? SubCategoryId { get; set; }

        public int? ManufacturerId { get; set; }

        public int? SupplierId { get; set; }
        
    }
}
