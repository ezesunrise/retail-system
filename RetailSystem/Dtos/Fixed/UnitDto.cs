using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class UnitDto
    {
        [Required]
        [StringLength(15)]
        public string Name { get; set; }

        public int? BusinessId { get; set; }
    }
}