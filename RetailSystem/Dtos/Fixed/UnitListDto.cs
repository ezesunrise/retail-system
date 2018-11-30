using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class UnitListDto
    {
        [Required]
        [StringLength(15)]
        public string Name { get; set; }

        public int? BusinessId { get; set; }
        public string BusinessName { get; set; }
    }
}