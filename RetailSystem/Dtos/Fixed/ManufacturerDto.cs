
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RetailSystem.Dtos
{
    public class ManufacturerDto : EntityDto
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        public string AdditionalInfo { get; set; }

        public int BusinessId { get; set; }
    }
}
